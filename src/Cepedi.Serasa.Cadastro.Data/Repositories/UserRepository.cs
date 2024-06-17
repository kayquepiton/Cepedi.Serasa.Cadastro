using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Cepedi.Serasa.Cadastro.Data;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> CreateUserAsync(UserEntity user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserEntity> GetUserAsync(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<UserEntity> GetUserByUsernameAsync(string username)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<UserEntity> UpdateUserAsync(UserEntity user)
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserEntity> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
    }
}
