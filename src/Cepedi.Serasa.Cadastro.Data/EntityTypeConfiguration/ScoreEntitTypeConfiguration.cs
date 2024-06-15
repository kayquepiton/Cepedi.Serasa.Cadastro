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

            // Configurar relacionamento um-para-um com PessoaEntity
            builder.HasOne(e => e.Pessoa)
                   .WithOne(p => p.Score)
                   .HasForeignKey<ScoreEntity>(e => e.IdPessoa)
                   .IsRequired(); // Define que é obrigatório ter uma pessoa associada a um score
        }
    }
}
