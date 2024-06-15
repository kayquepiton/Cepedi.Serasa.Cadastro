namespace Cepedi.Serasa.Cadastro.Shared.Responses.Movimentacao;
public record CriarMovimentacaoResponse(int Id, int IdTipoMovimentacao, int IdPessoa, DateTime DataHora, string? NomeEstabelecimento, decimal Valor);

