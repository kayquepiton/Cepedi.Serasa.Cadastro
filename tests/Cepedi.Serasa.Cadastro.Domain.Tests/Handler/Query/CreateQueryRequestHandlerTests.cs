using Cepedi.Serasa.Cadastro.Shared.Requests.Query;
using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Query;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Query;
public class CreateQueryRequestHandlerTests
{
    private readonly IQueryRepository _queryRepository = Substitute.For<IQueryRepository>();
    private readonly ILogger<CreateQueryRequestHandler> _logger = Substitute.For<ILogger<CreateQueryRequestHandler>>();
    private readonly CreateQueryRequestHandler _sut;

    public CreateQueryRequestHandlerTests()
    {
        _sut = new CreateQueryRequestHandler(_queryRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoCreateQuery_DeveRetornarSucesso()
    {
        // Arrange
        var Person = new PersonEntity
        {
            Id = 1,
            Name = "Pedro",
            CPF = "98765432110"
        };

        var request = new CreateQueryRequest
        {
            Status = true,
            Date = DateTime.Now,
            PersonId = Person.Id
        };

        var CreateQuery = new QueryEntity
        {
            Id = 1,
            Status = request.Status,
            Date = request.Date,
            PersonId = Person.Id
        };

        _queryRepository.GetPersonForQueryAsync(request.PersonId).Returns(Task.FromResult(Person));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CreateQueryResponse>>()
                    .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.PersonId.Should().Be(request.PersonId);
        result.Value.Status.Should().Be(request.Status);
        result.Value.Date.Should().Be(request.Date);

        await _queryRepository.Received(1).GetPersonForQueryAsync(request.PersonId);
        await _queryRepository.Received(1).CreateQueryAsync(Arg.Any<QueryEntity>());
    }

    [Fact]
    public async Task Handle_QuandoPersonNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var PersonId = 999; // ID inválido que não existe no repositório
        var request = new CreateQueryRequest
        {
            Status = true,
            Date = DateTime.Now,
            PersonId = PersonId
        };

        _queryRepository.GetPersonForQueryAsync(request.PersonId).Returns(Task.FromResult<PersonEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CreateQueryResponse>>()
            .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<AppException>()
            .Which.ErrorResult.Should().Be(RegistrationErrors.InvalidPersonId);

        await _queryRepository.Received(1).GetPersonForQueryAsync(request.PersonId);
        await _queryRepository.DidNotReceive().CreateQueryAsync(Arg.Any<QueryEntity>());
    }
}

