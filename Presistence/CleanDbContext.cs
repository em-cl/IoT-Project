using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Presistence
{
    public class CleanDbContext : DbContext
    {
        public DbSet<TraceLog> TraceLogs { get; set; } 
        public DbSet<Measurement> Measurements { get; set; }   
        public CleanDbContext(DbContextOptions options) : base(options)
        {

        }

    }
}
