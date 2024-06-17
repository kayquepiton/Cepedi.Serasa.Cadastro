namespace Cepedi.Serasa.Cadastro.Shared.Responses.Query
{
    public record GetQueryByIdResponse(int Id, int PersonId, bool Status, DateTime Date);
}
