using Cepedi.Serasa.Cadastro.Domain.Entities;

namespace Cepedi.Serasa.Cadastro.Domain.Repository;

public interface IPessoaRepository
{
    Task<PessoaEntity> ObterPessoaAsync(int id);
    Task<List<PessoaEntity>> ObterPessoasAsync();
    Task<PessoaEntity> CriarPessoaAsync(PessoaEntity pessoa);
    Task<PessoaEntity> AtualizarPessoaAsync(PessoaEntity pessoa);
    Task<PessoaEntity> ExcluirPessoaAsync(int id);
}
