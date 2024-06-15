﻿// <auto-generated />
using System;
using Cepedi.Serasa.Cadastro.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cepedi.Serasa.Cadastro.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Cepedi.Serasa.Cadastro.Domain.Entities.ConsultaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("IdPessoa");

                    b.ToTable("Consulta", (string)null);
                });

            modelBuilder.Entity("Cepedi.Serasa.Cadastro.Domain.Entities.MovimentacaoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoMovimentacao")
                        .HasColumnType("int");

                    b.Property<string>("NomeEstabelecimento")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("IdPessoa");

                    b.HasIndex("IdTipoMovimentacao");

                    b.ToTable("Movimentacao", (string)null);
                });

            modelBuilder.Entity("Cepedi.Serasa.Cadastro.Domain.Entities.PessoaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Pessoa", (string)null);
                });

            modelBuilder.Entity("Cepedi.Serasa.Cadastro.Domain.Entities.ScoreEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.Property<double>("Score")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("IdPessoa")
                        .IsUnique();

                    b.ToTable("Score", (string)null);
                });

            modelBuilder.Entity("Cepedi.Serasa.Cadastro.Domain.Entities.TipoMovimentacaoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NomeTipo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("TipoMovimentacao", (string)null);
                });

            modelBuilder.Entity("Cepedi.Serasa.Cadastro.Domain.Entities.UsuarioEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationRefreshToken")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("Cepedi.Serasa.Cadastro.Domain.Entities.ConsultaEntity", b =>
                {
                    b.HasOne("Cepedi.Serasa.Cadastro.Domain.Entities.PessoaEntity", "Pessoa")
                        .WithMany()
                        .HasForeignKey("IdPessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("Cepedi.Serasa.Cadastro.Domain.Entities.MovimentacaoEntity", b =>
                {
                    b.HasOne("Cepedi.Serasa.Cadastro.Domain.Entities.PessoaEntity", "Pessoa")
                        .WithMany()
                        .HasForeignKey("IdPessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cepedi.Serasa.Cadastro.Domain.Entities.TipoMovimentacaoEntity", "TipoMovimentacao")
                        .WithMany()
                        .HasForeignKey("IdTipoMovimentacao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");

                    b.Navigation("TipoMovimentacao");
                });

            modelBuilder.Entity("Cepedi.Serasa.Cadastro.Domain.Entities.ScoreEntity", b =>
                {
                    b.HasOne("Cepedi.Serasa.Cadastro.Domain.Entities.PessoaEntity", "Pessoa")
                        .WithOne("Score")
                        .HasForeignKey("Cepedi.Serasa.Cadastro.Domain.Entities.ScoreEntity", "IdPessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("Cepedi.Serasa.Cadastro.Domain.Entities.PessoaEntity", b =>
                {
                    b.Navigation("Score");
                });
#pragma warning restore 612, 618
        }
    }
}