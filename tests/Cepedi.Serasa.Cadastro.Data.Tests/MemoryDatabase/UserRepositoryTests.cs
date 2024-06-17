/*using Cepedi.Serasa.Cadastro.Data.Repositories;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Tests.MemoryDatabase;

public class UsernameRepositoryTests
{
    [Fact]
    public async Task Can_Get_Usernames_From_Database()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.User.Add(new UserEntity { Id = 1, Name = "Username1", Celular = "7199999999", Username = "teste", Cpf = "1235567789" });
            context.User.Add(new UserEntity { Id = 2, Name = "Username2", Celular = "7199999999", Username = "teste", Cpf = "1235567789" });
            context.SaveChanges();
        }

        // Act
        using (var context = new ApplicationDbContext(options))
        {
            var UsernameRepository = new UsuarioRepository(context);
            var Usernames = await UsernameRepository.GetUsuariosAsync();

            // Assert
            Assert.Equal(2, Usernames.Count);
            Assert.Equal("Username1", Usernames[0].Name);
            Assert.Equal("Username2", Usernames[1].Name);
        }
    }

}*/
