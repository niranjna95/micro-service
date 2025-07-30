using Mango.Services.EmailAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace Mongo.Services.EmailAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<EmailLogger> EmailLogger { get; set; }

       
    }
}
