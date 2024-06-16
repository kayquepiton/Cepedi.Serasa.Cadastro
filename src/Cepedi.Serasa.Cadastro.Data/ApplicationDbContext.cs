using System.Diagnostics.CodeAnalysis;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext : DbContext
{

    public DbSet<UserEntity> User { get; set; }
    public DbSet<QueryEntity> Query { get; set; }
    public DbSet<PersonEntity> Person { get; set; }
    public DbSet<TransactionTypeEntity> TransactionType { get; set; }
    public DbSet<ScoreEntity> Score { get; set; }
    public DbSet<TransactionEntity> Transaction { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        // Configurações adicionais para a entidade UsuarioEntity
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Login).IsRequired().HasMaxLength(100);
            entity.Property(e => e.RefreshToken).HasMaxLength(256);
            entity.Property(e => e.ExpirationRefreshToken).IsRequired();
        });
    }
}
