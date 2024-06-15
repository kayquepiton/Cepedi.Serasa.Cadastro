﻿using Cepedi.Serasa.Cadastro.Shared.Requests.Pessoa;
using Cepedi.Serasa.Cadastro.Shared.Responses.Pessoa;
using Cepedi.Serasa.Cadastro.Shared.Validators.Pessoa;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Pessoa;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.Pessoa;

public class CriarPessoaRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<CriarPessoaRequestHandler> _logger;
    private readonly CriarPessoaRequestHandler _sut;

    public CriarPessoaRequestHandlerTests()
    {
        _pessoaRepository = Substitute.For<IPessoaRepository>();
        _logger = Substitute.For<ILogger<CriarPessoaRequestHandler>>();
        _sut = new CriarPessoaRequestHandler(_pessoaRepository, _logger);
    }

    [Fact]
    public async Task QuandoCriarPessoaDeveRetornarSucesso()
    {
        //Arrange
        var pessoaRequest = new CriarPessoaRequest
        {
            Nome = "Fernando Lima",
            CPF = "43669795006"
        };

        var pessoa = new PessoaEntity
        {
            Nome = pessoaRequest.Nome,
            CPF = pessoaRequest.CPF
        };

        _pessoaRepository.CriarPessoaAsync(Arg.Is<PessoaEntity>(pessoa => pessoa.Nome == pessoaRequest.Nome
        && pessoa.CPF == pessoaRequest.CPF)).Returns(pessoa);

        //Act
        var result = await _sut.Handle(pessoaRequest, CancellationToken.None);

        //Assert
        result.Should().BeOfType<Result<CriarPessoaResponse>>()
            .Which.Value.Nome.Should().Be(pessoaRequest.Nome);

        result.Should().BeOfType<Result<CriarPessoaResponse>>()
            .Which.Value.CPF.Should().Be(pessoaRequest.CPF);

        await _pessoaRepository.Received(1)
            .CriarPessoaAsync(Arg.Is<PessoaEntity>(pessoa => pessoa.Nome == pessoaRequest.Nome && pessoa.CPF == pessoaRequest.CPF));
    }

    [Fact]
    public async Task QuandoCriarPessoaComDataInvalidosDeveRetornarErro()
    {
        //Arrange
        var pessoaRequest = new CriarPessoaRequest
        {
            Nome = "Ze",
            CPF = "123"
        };

        var validator = new CriarPessoaRequestValidator();

        //Act
        var validationResult = validator.Validate(pessoaRequest);
        var result = await _sut.Handle(pessoaRequest, CancellationToken.None);

        //Assert
        await _pessoaRepository.Received(1)
            .CriarPessoaAsync(Arg.Is<PessoaEntity>(pessoa => pessoa.Nome == pessoaRequest.Nome && pessoa.CPF == pessoaRequest.CPF));

        validationResult.IsValid.Should().BeFalse();
        result.Should().BeOfType<Result<CriarPessoaResponse>>();
        result.IsSuccess.Should().BeTrue();
    }
}