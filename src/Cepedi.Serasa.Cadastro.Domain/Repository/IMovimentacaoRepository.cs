using Cepedi.Serasa.Cadastro.Domain.Entities;

namespace Cepedi.Serasa.Cadastro.Domain.Repository;

public interface IMovimentacaoRepository
{
    Task<MovimentacaoEntity> CriarMovimentacaoAsync(MovimentacaoEntity movimentacao);
    Task<MovimentacaoEntity> ObterMovimentacaoAsync(int id);
    Task<List<MovimentacaoEntity>> ObterTodasMovimentacoesAsync();
    Task<MovimentacaoEntity> AtualizarMovimentacaoAsync(MovimentacaoEntity movimentacao);
    Task<MovimentacaoEntity> DeletarMovimentacaoAsync(int id);
}