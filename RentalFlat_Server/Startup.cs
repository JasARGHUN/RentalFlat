using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using BusinessLogic.Repository;
using BusinessLogic.Repository.IRepository;
using DataAccess.Data;
using RentalFlat_Server.Services;
using RentalFlat_Server.Services.IServices;


namespace RentalFlat_Server
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<ApplicationDbContext>().
                AddDefaultTokenProviders().AddDefaultUI();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IFlatRepository, FlatRepository>();
            services.AddScoped<IHomepageInfoRepository, HomepageInfoRepository>();
            services.AddScoped<IFlatImagesRepository, FlatImagesRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IFileUpload, FileUpload>();
            services.AddScoped<IImageUpload, ImageUpload>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IInfoBlockRepository, InfoBlockRepository>();

            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddServerSideBlazor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            dbInitializer.Initialize(); // Seed admin user and another roles to database.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
