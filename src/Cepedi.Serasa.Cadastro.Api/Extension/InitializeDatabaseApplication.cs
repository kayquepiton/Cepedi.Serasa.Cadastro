﻿using Cepedi.Serasa.Cadastro.IoC;

namespace Cepedi.Serasa.Cadastro.Api.Extension;
public static class InitializeDatabaseApplication
{
    public static async Task InitialiseDatabaseAsync(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}
