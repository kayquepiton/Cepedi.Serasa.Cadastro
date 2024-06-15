﻿using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Repositories;
public class PessoaRepository : IPessoaRepository
{
    private readonly ApplicationDbContext _context;

    public PessoaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PessoaEntity> AtualizarPessoaAsync(PessoaEntity pessoa)
    {
        _context.Pessoa.Update(pessoa);
        await _context.SaveChangesAsync();

        return pessoa;
    }

    public async Task<PessoaEntity> CriarPessoaAsync(PessoaEntity pessoa)
    {
        await _context.Pessoa.AddAsync(pessoa);
        await _context.SaveChangesAsync();

        return pessoa;
    }

    public async Task<PessoaEntity?> ExcluirPessoaAsync(int id)
    {
        var PessoaEntity = await ObterPessoaAsync(id);
        if (PessoaEntity == null) return null;

        _context.Pessoa.Remove(PessoaEntity);
        await _context.SaveChangesAsync();
        return PessoaEntity;
    }

    public async Task<PessoaEntity> ObterPessoaAsync(int id)
        => await _context.Pessoa.Where(pessoa => pessoa.Id == id).FirstOrDefaultAsync();

    public async Task<List<PessoaEntity>> ObterPessoasAsync()
        => await _context.Pessoa.ToListAsync();

}