using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Exececoes;
using Cepedi.Serasa.Cadastro.Shared.Usuario.Requests;
using Cepedi.Serasa.Cadastro.Shared.Usuario.Responses;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Usuario.Handlers
{
    public class CriarUsuarioRequestHandler : IRequestHandler<CriarUsuarioRequest, Result<CriarUsuarioResponse>>
    {
        private readonly ILogger<CriarUsuarioRequestHandler> _logger;
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarUsuarioRequestHandler(IUsuarioRepository usuarioRepository, ILogger<CriarUsuarioRequestHandler> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        public async Task<Result<CriarUsuarioResponse>> Handle(CriarUsuarioRequest request, CancellationToken cancellationToken)
        {
            // Verificar se o Login já está sendo utilizado
            var existingLogin = await _usuarioRepository.ObterUsuarioPorLoginAsync(request.Login);
            if (existingLogin != null)
            {
                // Se o Login já está sendo utilizado, retornar um erro indicando duplicidade
                return Result.Error<CriarUsuarioResponse>(
                    new ExcecaoAplicacao(CadastroErros.LoginDuplicado));
            }

            // Calcular o hash SHA256 da senha recebida
            string hashedPassword;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(request.Senha));
                hashedPassword = Convert.ToBase64String(hashedBytes);
            }

            var usuario = new UsuarioEntity()
            {
                Nome = request.Nome,
                Login = request.Login,
                Senha = hashedPassword  // Salvar a senha como hash SHA256
            };

            await _usuarioRepository.CriarUsuarioAsync(usuario);

            return Result.Success(new CriarUsuarioResponse(usuario.Id, usuario.Nome));
        }
    }
}
