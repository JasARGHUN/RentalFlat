using Common;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalFlat_Server.Services.IServices;
using System.Linq;

namespace RentalFlat_Server.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_context.Database.GetPendingMigrations().Count() > 0)
            {
                _context.Database.Migrate();
            }

            if (_context.Roles.Any(x => x.Name == StaticDetails.Role_Admin))
                return;

            _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin))
                .GetAwaiter().GetResult();

            _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Employee))
                .GetAwaiter().GetResult();

            _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Customer))
                .GetAwaiter().GetResult();

            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin@mail.com",
                Email = "admin@mail.com",
                EmailConfirmed = true
            }, "111111Test$").GetAwaiter().GetResult();

            IdentityUser user = _context.Users.FirstOrDefault(x => x.Email == "admin@mail.com");

            _userManager.AddToRoleAsync(user, StaticDetails.Role_Admin).GetAwaiter().GetResult();
        }
    }
}
