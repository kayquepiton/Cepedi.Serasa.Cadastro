namespace Cepedi.Serasa.Cadastro.Domain.Entities
{
    public class PersonEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string CPF { get; set; }
        public ScoreEntity? Score { get; set; }
    }
}