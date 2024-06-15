﻿using System.Diagnostics.CodeAnalysis;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext : DbContext
{

    public DbSet<UsuarioEntity> Usuario { get; set; } = default!;
    public DbSet<ConsultaEntity> Consulta { get; set; } = default!;
    public DbSet<PessoaEntity> Pessoa { get; set; } = default!;
    public DbSet<TipoMovimentacaoEntity> TipoMovimentacao { get; set; } = default!;
    public DbSet<ScoreEntity> Score { get; set; } = default!;
    public DbSet<MovimentacaoEntity> Movimentacao { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        // Configurações adicionais para a entidade UsuarioEntity
        modelBuilder.Entity<UsuarioEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Login).IsRequired().HasMaxLength(100);
            entity.Property(e => e.RefreshToken).HasMaxLength(256);
            entity.Property(e => e.ExpirationRefreshToken).IsRequired();
        });
    }
}