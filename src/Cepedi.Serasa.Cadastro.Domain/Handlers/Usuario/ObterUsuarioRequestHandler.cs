using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Usuario.Requests;
using Cepedi.Serasa.Cadastro.Shared.Usuario.Responses;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Usuario.Handlers;
public class ObterUsuarioRequestHandler : IRequestHandler<ObterUsuarioRequest, Result<ObterUsuarioResponse>>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<ObterUsuarioRequestHandler> _logger;

    public ObterUsuarioRequestHandler(ILogger<ObterUsuarioRequestHandler> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Result<ObterUsuarioResponse>> Handle(ObterUsuarioRequest request, CancellationToken cancellationToken)
    {
        // Verificar se o usuário existe com base no ID fornecido na solicitação
        var usuarioEntity = await _usuarioRepository.ObterUsuarioAsync(request.Id);

        // Se o usuário não existe, retornar um erro indicando a falta de resultados
        if (usuarioEntity == null)
        {
            return Result.Error<ObterUsuarioResponse>(new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdUsuarioInvalido));
        }

        // Criar uma resposta com os Data do usuário obtido
        var response = new ObterUsuarioResponse(
            usuarioEntity.Id,
            usuarioEntity.Nome,
            usuarioEntity.Login
            // Adicionar outras propriedades conforme necessário
        );

        // Retornar um resultado de sucesso contendo a resposta do usuário obtido
        return Result.Success(response);
    }
}
