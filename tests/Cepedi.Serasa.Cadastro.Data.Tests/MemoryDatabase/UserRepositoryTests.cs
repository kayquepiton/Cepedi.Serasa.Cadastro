/*using Cepedi.Serasa.Cadastro.Data.Repositories;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Tests.MemoryDatabase;

public class LoginRepositoryTests
{
    [Fact]
    public async Task Can_Get_Logins_From_Database()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.Usuario.Add(new UsuarioEntity { Id = 1, Nome = "Login1", Celular = "7199999999", Login = "teste", Cpf = "1235567789" });
            context.Usuario.Add(new UsuarioEntity { Id = 2, Nome = "Login2", Celular = "7199999999", Login = "teste", Cpf = "1235567789" });
            context.SaveChanges();
        }

        // Act
        using (var context = new ApplicationDbContext(options))
        {
            var LoginRepository = new UsuarioRepository(context);
            var Logins = await LoginRepository.GetUsuariosAsync();

            // Assert
            Assert.Equal(2, Logins.Count);
            Assert.Equal("Login1", Logins[0].Nome);
            Assert.Equal("Login2", Logins[1].Nome);
        }
    }

}*/
