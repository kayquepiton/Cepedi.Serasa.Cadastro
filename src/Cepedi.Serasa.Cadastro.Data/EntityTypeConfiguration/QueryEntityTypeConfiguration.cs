using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Data.EntityTypeConfiguration
{
    public class QueryEntityTypeConfiguration : IEntityTypeConfiguration<QueryEntity>
    {
        public void Configure(EntityTypeBuilder<QueryEntity> builder)
        {
            builder.ToTable("Query");
            builder.HasKey(query => query.Id);

            builder.Property(query => query.PersonId).IsRequired();
            builder.Property(query => query.Date).IsRequired();
            builder.Property(query => query.Status).IsRequired();

            builder.HasOne(query => query.Person)
                   .WithMany()
                   .HasForeignKey(query => query.PersonId)
                   .IsRequired();
        }
    }
}
