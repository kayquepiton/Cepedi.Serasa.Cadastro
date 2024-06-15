﻿using Cepedi.Serasa.Cadastro.Data;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Repositories;

public class TipoMovimentacaoRepository : ITipoMovimentacaoRepository
{
    private readonly ApplicationDbContext _context;
    public TipoMovimentacaoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TipoMovimentacaoEntity> AtualizarTipoMovimentacaoAsync(TipoMovimentacaoEntity tipoMovimentacao)
    {
        _context.TipoMovimentacao.Update(tipoMovimentacao);

        await _context.SaveChangesAsync();

        return tipoMovimentacao;
    }

    public async Task<TipoMovimentacaoEntity> CriarTipoMovimentacaoAsync(TipoMovimentacaoEntity tipoMovimentacao)
    {
        _context.TipoMovimentacao.Add(tipoMovimentacao);
        await _context.SaveChangesAsync();
        return tipoMovimentacao;
    }

    public async Task<TipoMovimentacaoEntity?> DeletarTipoMovimentacaoAsync(int id)
    {
        var tipoMovimentacaoEntity = await ObterTipoMovimentacaoAsync(id);
        if (tipoMovimentacaoEntity == null) return null;

        _context.TipoMovimentacao.Remove(tipoMovimentacaoEntity);
        await _context.SaveChangesAsync();
        return tipoMovimentacaoEntity;
    }

    public async Task<List<TipoMovimentacaoEntity>> ObterTodosTiposMovimentacaoAsync()
    {
        return await _context.Set<TipoMovimentacaoEntity>().ToListAsync();
    }

    public async Task<TipoMovimentacaoEntity> ObterTipoMovimentacaoAsync(int id)
    {
        return await
                _context.TipoMovimentacao.Where(e => e.Id == id).FirstOrDefaultAsync();
    }
}