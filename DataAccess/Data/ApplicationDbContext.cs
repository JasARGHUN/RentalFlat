using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Flat> Flats { get; set; }
        public DbSet<HomepageInfo> HomepageInfos { get; set; }
        public DbSet<FlatImage> FlatImages { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<HomeImage> HomeImages { get; set; }
        public DbSet<InfoBlock> InfoBlocks { get; set; }
    }
}
