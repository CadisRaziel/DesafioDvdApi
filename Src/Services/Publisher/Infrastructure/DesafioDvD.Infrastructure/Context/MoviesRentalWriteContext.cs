
using DesafioDvD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioDvD.Infrastructure.Context
{
    public class MoviesRentalWriteContext : DbContext
    {
        public MoviesRentalWriteContext()
        {
            
        }

        public MoviesRentalWriteContext(DbContextOptions<MoviesRentalWriteContext> options) : base(options)
        {
            
        }

        public DbSet<Director> Directors { get; set; }
        public DbSet<Dvd> Dvds { get; set; }
    }
}
