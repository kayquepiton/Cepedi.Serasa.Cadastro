using Cepedi.Serasa.Cadastro.Shared.Enums;
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

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Query
{
    public class UpdateQueryRequestHandlerTests
    {
        private readonly IQueryRepository _QueryRepository = Substitute.For<IQueryRepository>();
        private readonly ILogger<UpdateQueryRequestHandler> _logger = Substitute.For<ILogger<UpdateQueryRequestHandler>>();
        private readonly UpdateQueryRequestHandler _sut;

        public UpdateQueryRequestHandlerTests()
        {
            _sut = new UpdateQueryRequestHandler(_QueryRepository, _logger);
        }

        [Fact]
        public async Task Handle_WhenUpdatingQuery_ShouldReturnSuccess()
        {
            // Arrange
            var request = new UpdateQueryRequest
            {
                Id = 1,
                Status = true
            };

            var existingQuery = new QueryEntity
            {
                Id = request.Id,
                PersonId = 1,
                Status = false,
                Date = DateTime.UtcNow
            };

            _QueryRepository.GetQueryAsync(request.Id).Returns(Task.FromResult(existingQuery));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<UpdateQueryResponse>>()
                    .Which.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(existingQuery.Id);
            result.Value.PersonId.Should().Be(existingQuery.PersonId);
            result.Value.Status.Should().Be(request.Status);
            result.Value.Date.Should().Be(existingQuery.Date);

            await _QueryRepository.Received(1).GetQueryAsync(request.Id);
            await _QueryRepository.Received(1).UpdateQueryAsync(Arg.Is<QueryEntity>(
                c => c.Id == request.Id &&
                        c.Status == request.Status
            ));
        }

        [Fact]
        public async Task Handle_WhenQueryDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var request = new UpdateQueryRequest
            {
                Id = 1,
                Status = true
            };

            _QueryRepository.GetQueryAsync(request.Id).Returns(Task.FromResult<QueryEntity>(null));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<UpdateQueryResponse>>()
                    .Which.IsSuccess.Should().BeFalse();

            result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                    .Which.ErrorResult.Should().Be(RegistrationErrors.InvalidQueryId);
        }
    }
}
