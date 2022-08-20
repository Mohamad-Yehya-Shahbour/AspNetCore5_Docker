using colours.Models;
using Microsoft.EntityFrameworkCore;

namespace colours.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Colour> Colours { get; set; }

    }
    
}
