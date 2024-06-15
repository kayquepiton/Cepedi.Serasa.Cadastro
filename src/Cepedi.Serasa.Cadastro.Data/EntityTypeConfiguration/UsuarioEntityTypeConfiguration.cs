using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Data.EntityTypeConfiguration
{
    public class UsuarioEntityTypeConfiguration : IEntityTypeConfiguration<UsuarioEntity>
    {
        public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(c => c.Id); // Define a chave primária

            builder.Property(c => c.Nome).IsRequired();
            builder.Property(c => c.Login).IsRequired();
            builder.Property(c => c.Senha).IsRequired();
        }
    }
}
