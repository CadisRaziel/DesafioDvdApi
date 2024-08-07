using DesafioDvD.Domain.Entities;
using DesafioDvD.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDvD.Infrastructure.Config
{
    public class DvdsConfig : IEntityTypeConfiguration<Dvd> // <> Tem que ser nosso dominio
    {
        public void Configure(EntityTypeBuilder<Dvd> builder)
        {
            builder.ToTable("Dvds"); //-> nome da tabela no sql

            builder.HasKey(x => x.Id); //-> chave primaria

            builder.HasIndex(x => x.Title) //-> e possivel usar o EF para criar index no banco de dados, index facilita a pesqusia no banco relacional
                //Exemplo, eu tenho o filme `IT`, o titulo comeca com I, a query geralmente procura de A ate Z passando todas letras do alfabeto
                //quando definimos o indice, ele vai procurar apenas pela primeira letra,ou seja o `IT` tem a primeira letra `I` com isso ele vai ignorar todas as outras letras do alfabeto e vai procurar direto no `I`
                //Nos nao precisariamos usar pq vamos usar query de consulta do mongoDb, porem quando tiver apenas EF e interessante usar
                .IsUnique();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(Dvd.MAX_TITLE_LENGTH);

            builder.Property(x => x.Copies)
                .IsRequired(); 

            builder.Property(x => x.Available)
                .IsRequired();

            builder.Property(x => x.Published)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
              .IsRequired();

            builder.Property(x => x.UpdatedAt)
              .IsRequired();

            builder.Property(x => x.DirectorId)
              .IsRequired();

            //Relacionamento entre tabelas
            //HasOne Eu tenho um diretor
            //WithMany Com muitos Dvds
            builder.HasOne(x => x.Director)
                .WithMany(d => d.Dvds)
                .HasForeignKey(x => x.DirectorId); //-> Repare que no dominio eu passo DirectorId e logo abaixo eu crio o prop do dominio do Director

            //Convertendo o Genre(Enum), no banco o Enum e salvo como int, porem vamos converter para string
            //z => (EGenre)Enum.Parse(typeof(EGenre), z) -> Quando chegar no banco ele nao vai ter 1 => action, ele vai ter apenas o `action`
            builder.Property(x => x.Genre)
                .HasConversion(y => y.ToString(), z => (EGenre)Enum.Parse(typeof(EGenre), z)); 
        }
    }
}
