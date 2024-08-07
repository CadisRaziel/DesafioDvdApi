
using DesafioDvD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDvD.Infrastructure.Config
{
    public class DirectorsConfig : IEntityTypeConfiguration<Director>
    {
        //Ao inves de eu fazer essas configuracoes na classe de configuracao do banco, eu externalizo para essa classe fazendo o de cada dominio separado
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            //Modelagem do banco

            builder.ToTable("Directors"); //-> Nome da tabela no banco

            builder.HasKey(x =>  x.Id); //-> Definindo chave primaria

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(Director.MAX_LENGTH);

            builder.Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(Director.MAX_LENGTH);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired();

            //Relacionamento
            builder.HasMany(x => x.Dvds)
                .WithOne(d => d.Director);
        }
    }
}
