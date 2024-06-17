using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Data.EntityTypeConfiguration
{
    public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("Transaction");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.PersonId).IsRequired();
            builder.Property(c => c.DateTime).IsRequired(); 
            builder.Property(c => c.TransactionTypeId).IsRequired();
            builder.Property(c => c.Value).IsRequired();

            builder.Property(c => c.EstablishmentName).HasMaxLength(255); 


            builder.HasOne(c => c.Person)
                   .WithMany()  
                   .HasForeignKey(c => c.PersonId) 
                   .IsRequired();


            builder.HasOne(c => c.TransationType)
                   .WithMany()  // 
                   .HasForeignKey(c => c.TransactionTypeId)
                   .IsRequired();  // 
        }
    }
}
