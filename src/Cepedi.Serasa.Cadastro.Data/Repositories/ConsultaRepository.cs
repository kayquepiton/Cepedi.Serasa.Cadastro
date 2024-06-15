using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Repositories;
public class ConsultaRepository : IConsultaRepository
{
    private readonly ApplicationDbContext _context;

    public ConsultaRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ConsultaEntity> AtualizarConsultaAsync(ConsultaEntity status)
    {
        _context.Consulta.Update(status);

        await _context.SaveChangesAsync();

        return status;
    }

    public async Task<ConsultaEntity> CriarConsultaAsync(ConsultaEntity status)
    {
        _context.Consulta.Add(status);

        await _context.SaveChangesAsync();

        return status;
    }

    public async Task<ConsultaEntity> ObterConsultaAsync(int id)
    {
        return await
            _context.Consulta.Where(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<ConsultaEntity>> ObterTodasConsultasAsync()
    {
        return await _context.Set<ConsultaEntity>().ToListAsync();
    }

    public async Task<PessoaEntity> ObterPessoaConsultaAsync(int id)
    {
        return await
            _context.Pessoa.Where(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<ConsultaEntity> DeletarConsultaAsync(int id)
    {
        var consultaEntity = await ObterConsultaAsync(id);

        if (consultaEntity == null)
        {
            return null;
        }

        _context.Consulta.Remove(consultaEntity);

        await _context.SaveChangesAsync();

        return consultaEntity;
    }

}