using Microsoft.EntityFrameworkCore;
using KoreliMobilyaDeneme.Models;

namespace KoreliMobilyaDeneme.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Ardıdnan hangi dbcontext olduğunu options olarak belirlemek için yapıyoruz	
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<KoreliMobilyaDeneme.Models.Category> Category { get; set; }
        //Burda ise DB deki ismi Products tablomuzun onu yazıyoruz ve models içindeki classımızla eşliyoruz onun adıda product

        public DbSet<Admin> Admins { get; set; }

    }
}
