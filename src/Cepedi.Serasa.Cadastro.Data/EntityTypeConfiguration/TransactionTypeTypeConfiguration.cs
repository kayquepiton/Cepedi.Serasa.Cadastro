using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Data.EntityTypeConfiguration
{
    public class TransactionTypeTypeConfiguration : IEntityTypeConfiguration<TransactionTypeEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionTypeEntity> builder)
        {
            builder.ToTable("TransactionType");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.TypeName).IsRequired().HasMaxLength(255);
        }
    }
}
