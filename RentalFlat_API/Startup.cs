using BusinessLogic.Repository;
using BusinessLogic.Repository.IRepository;
using DataAccess.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using RentalFlat_API.Helpers;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalFlat_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<ApplicationDbContext>().
                AddDefaultTokenProviders();

            //Include some sections from appsettings.json for APISettings.
            var appSettingsSection = Configuration.GetSection("APISettings");
            services.Configure<APISettings>(appSettingsSection);

            //MailJSetSettings
            services.Configure<MailJetSettings>(Configuration.GetSection("MailJetSettings"));

            var apiSettings = appSettingsSection.Get<APISettings>();
            var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudience = apiSettings.ValidAudience,
                    ValidIssuer = apiSettings.ValidIssuer,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IFlatRepository, FlatRepository>();
            services.AddScoped<IHomepageInfoRepository, HomepageInfoRepository>();
            services.AddScoped<IFlatImagesRepository, FlatImagesRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IInfoBlockRepository, InfoBlockRepository>();

            services.AddCors(x => x.AddPolicy("RentalFlat", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentalApp.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please Bearer and then token in the field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["ApiKey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentalFlat_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors("RentalFlat");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
