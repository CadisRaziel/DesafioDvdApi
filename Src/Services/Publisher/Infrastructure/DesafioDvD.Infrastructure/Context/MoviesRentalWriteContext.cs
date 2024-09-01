
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Estou pegando todas propriedades do tipo string e to setando elas como `varchar(100), caso ela nao tenha o valor definido 
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            //Aqui eu pego os meus config `dentro da pasta Config` aonde eu criei especificacoes que eu quero para cada tabela
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoviesRentalWriteContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
