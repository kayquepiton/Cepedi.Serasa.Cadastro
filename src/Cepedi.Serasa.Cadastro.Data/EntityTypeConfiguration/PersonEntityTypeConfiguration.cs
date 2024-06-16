using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Data.EntityTypeConfiguration
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<PersonEntity>
    {
        public void Configure(EntityTypeBuilder<PersonEntity> builder)
        {
            builder.ToTable("Person");
            builder.HasKey(Person => Person.Id);

            builder.Property(Person => Person.Name).IsRequired().HasMaxLength(100);
            builder.Property(Person => Person.CPF).IsRequired().HasMaxLength(11);

        }
    }
}
