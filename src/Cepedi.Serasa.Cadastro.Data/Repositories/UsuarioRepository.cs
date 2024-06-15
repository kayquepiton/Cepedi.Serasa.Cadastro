using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UsuarioEntity> CriarUsuarioAsync(UsuarioEntity usuario)
    {
        _context.Usuario.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<UsuarioEntity> ObterUsuarioAsync(int id)
    {
        return await _context.Usuario.FindAsync(id);
    }

    public async Task<UsuarioEntity> ObterUsuarioPorLoginAsync(string Login) // Implementação do novo método
    {
        return await _context.Usuario.FirstOrDefaultAsync(u => u.Login == Login);
    }

    public async Task<List<UsuarioEntity>> ObterTodosUsuariosAsync()
    {
        return await _context.Usuario.ToListAsync();
    }

    public async Task<UsuarioEntity> AtualizarUsuarioAsync(UsuarioEntity usuario)
    {
        _context.Usuario.Update(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task DeletarUsuarioAsync(int id)
    {
        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<UsuarioEntity> ObterUsuarioPorRefreshTokenAsync(string refreshToken)
    {
        return await _context.Usuario.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }
}
