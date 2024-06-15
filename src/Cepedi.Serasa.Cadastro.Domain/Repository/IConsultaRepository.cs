using Cepedi.Serasa.Cadastro.Domain.Entities;

namespace Cepedi.Serasa.Cadastro.Domain.Repository;

public interface IConsultaRepository
{
    Task<ConsultaEntity> CriarConsultaAsync(ConsultaEntity Status);
    Task<ConsultaEntity> ObterConsultaAsync(int id);
    Task<List<ConsultaEntity>> ObterTodasConsultasAsync();
    Task<ConsultaEntity> AtualizarConsultaAsync(ConsultaEntity Status);
    Task<PessoaEntity> ObterPessoaConsultaAsync(int id);
    Task<ConsultaEntity> DeletarConsultaAsync(int id);
}