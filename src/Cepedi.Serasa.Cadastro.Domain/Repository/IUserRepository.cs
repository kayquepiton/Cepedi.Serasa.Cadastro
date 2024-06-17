using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Cepedi.Serasa.Cadastro.Domain.Entities;

namespace Cepedi.Serasa.Cadastro.Domain.Repository
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetAllUsersAsync();
        Task<UserEntity> GetUserAsync(int id);
        Task<UserEntity> GetUserByUsernameAsync(string username); // New method
        Task<UserEntity> GetUserByRefreshTokenAsync(string refreshToken);
        Task<UserEntity> CreateUserAsync(UserEntity user);
        Task<UserEntity> UpdateUserAsync(UserEntity user);
        Task DeleteUserAsync(int id);
    }
}
