using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Usuario.Requests;
using Cepedi.Serasa.Cadastro.Shared.Usuario.Responses;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers;

public class ObterTodosUsuariosRequestHandler : IRequestHandler<ObterTodosUsuariosRequest, Result<List<ObterTodosUsuariosResponse>>>
{
    private readonly ILogger<ObterTodosUsuariosRequestHandler> _logger;
    private readonly IUsuarioRepository _usuarioRepository;

    public ObterTodosUsuariosRequestHandler(ILogger<ObterTodosUsuariosRequestHandler> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Result<List<ObterTodosUsuariosResponse>>> Handle(ObterTodosUsuariosRequest request, CancellationToken cancellationToken)
    {
        // Obter todos os usuários do repositório
        var usuarios = await _usuarioRepository.ObterTodosUsuariosAsync();

        // Verificar se a lista de usuários é nula
        if (usuarios == null)
        {
            // Se não houver usuários, retornar um erro indicando a falta de resultados
            return Result.Error<List<ObterTodosUsuariosResponse>>(
                new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.ListaUsuariosVazia));
        }

        // Inicializar uma lista para armazenar as respostas dos usuários
        var response = new List<ObterTodosUsuariosResponse>();

        // Para cada usuário obtido, criar uma resposta correspondente
        foreach (var usuario in usuarios)
        {
            response.Add(new ObterTodosUsuariosResponse(
                usuario.Id,
                usuario.Nome,
                usuario.Login
                // Adicionar outras propriedades conforme necessário
            ));
        }

        // Retornar um resultado de sucesso contendo a lista de respostas dos usuários
        return Result.Success(response);
    }
}
