namespace Cepedi.Serasa.Cadastro.Shared.Responses.Consulta;

public record DeletarConsultaResponse(int Id, int IdPessoa, bool Status, DateTime Data);