using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Exececoes;
using Cepedi.Serasa.Cadastro.Shared.Requests.Pessoa;
using Cepedi.Serasa.Cadastro.Shared.Responses.Pessoa;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Pessoa;

public class CriarPessoaRequestHandler
    : IRequestHandler<CriarPessoaRequest, Result<CriarPessoaResponse>>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<CriarPessoaRequestHandler> _logger;

    public CriarPessoaRequestHandler(IPessoaRepository pessoaRepository, ILogger<CriarPessoaRequestHandler> logger)
    {
        _pessoaRepository = pessoaRepository;
        _logger = logger;
    }

    public async Task<Result<CriarPessoaResponse>> Handle(CriarPessoaRequest request, CancellationToken cancellationToken)
    {
        var pessoa = new PessoaEntity
        {
            Nome = request.Nome,
            CPF = request.CPF
        };

        await _pessoaRepository.CriarPessoaAsync(pessoa);

        return Result.Success(new CriarPessoaResponse(pessoa.Id, pessoa.Nome, pessoa.CPF));
    }
}
