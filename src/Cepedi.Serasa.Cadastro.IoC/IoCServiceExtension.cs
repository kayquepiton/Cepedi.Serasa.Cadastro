﻿using System.Diagnostics.CodeAnalysis;
using Cepedi.Serasa.Cadastro.Data.Repositories;
using Cepedi.Serasa.Cadastro.Data;
using Cepedi.Serasa.Cadastro.Domain.Pipelines;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cepedi.Serasa.Cadastro.Shared;

namespace Cepedi.Serasa.Cadastro.IoC
{
    [ExcludeFromCodeCoverage]
    public static class IoCServiceExtension
    {
        public static void ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            ConfigureDbContext(services, configuration);
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExcecaoPipeline<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidacaoComportamento<,>));
            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            ConfigurarFluentValidation(services);
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<ITipoMovimentacaoRepository, TipoMovimentacaoRepository>();
            services.AddScoped<IScoreRepository, ScoreRepository>();
            services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
            //services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();
        }

        private static void ConfigurarFluentValidation(IServiceCollection services)
        {
            var abstractValidator = typeof(AbstractValidator<>);
            var validadores = typeof(IValida)
                .Assembly
                .DefinedTypes
                .Where(type => type.BaseType?.IsGenericType is true &&
                type.BaseType.GetGenericTypeDefinition() ==
                abstractValidator)
                .Select(Activator.CreateInstance)
                .ToArray();

            foreach (var validator in validadores)
            {
                services.AddSingleton(validator!.GetType().BaseType!, validator);
            }
        }

        private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                //options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            });

            services.AddScoped<ApplicationDbContextInitialiser>();
        }
    }
}
