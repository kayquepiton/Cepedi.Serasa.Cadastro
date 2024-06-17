using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Data.EntityTypeConfiguration
{
    public class ScoreEntityTypeConfiguration : IEntityTypeConfiguration<ScoreEntity>
    {
        public void Configure(EntityTypeBuilder<ScoreEntity> builder)
        {
            builder.ToTable("Score");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Score).IsRequired();

            builder.HasOne(e => e.Person)
                   .WithOne(p => p.Score)
                   .HasForeignKey<ScoreEntity>(e => e.PersonId)
                   .IsRequired(); 
        }
    }
}
