using Cepedi.Serasa.Cadastro.Data;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Repositories;

public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly ApplicationDbContext _context;
    public MovimentacaoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MovimentacaoEntity> CriarMovimentacaoAsync(MovimentacaoEntity movimentacao)
    {
        _context.Movimentacao.Add(movimentacao);
        await _context.SaveChangesAsync();
        return movimentacao;
    }

    public async Task<MovimentacaoEntity> ObterMovimentacaoAsync(int id)
    {
        return await
                _context.Movimentacao.Where(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<MovimentacaoEntity>> ObterTodasMovimentacoesAsync()
    {
        return await _context.Set<MovimentacaoEntity>().ToListAsync();
    }

    public async Task<MovimentacaoEntity> AtualizarMovimentacaoAsync(MovimentacaoEntity movimentacao)
    {
        _context.Movimentacao.Update(movimentacao);

        await _context.SaveChangesAsync();

        return movimentacao;
    }
    
    public async Task<MovimentacaoEntity?> DeletarMovimentacaoAsync(int id)
    {
        var movimentacaoEntity = await ObterMovimentacaoAsync(id);
        if (movimentacaoEntity == null) return null;

        _context.Movimentacao.Remove(movimentacaoEntity);
        await _context.SaveChangesAsync();
        return movimentacaoEntity;
    }

}