using Microsoft.EntityFrameworkCore;
using TripsLogApp.Models;

namespace TripsLogApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; }
    }
}
