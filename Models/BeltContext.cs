using Microsoft.EntityFrameworkCore;
 
namespace CBelt.Models
{
    public class BeltContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BeltContext(DbContextOptions<BeltContext> options) : base(options) { }
        public DbSet<User> Users{get;set;}
        public DbSet<RSVP> RSVPs{get;set;}
        public DbSet<Activity> Activities{get;set;}
    }
}
