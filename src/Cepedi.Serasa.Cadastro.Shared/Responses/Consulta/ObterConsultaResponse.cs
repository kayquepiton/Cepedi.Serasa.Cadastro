namespace Cepedi.Serasa.Cadastro.Shared.Responses.Consulta;

public record ObterConsultaResponse(int Id, int IdPessoa, bool Status, DateTime Data);