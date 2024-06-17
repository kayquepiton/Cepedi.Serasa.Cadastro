using Cepedi.Serasa.Cadastro.Shared.Requests.Query;
using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Query;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Query
{
    public class DeleteQueryByIdRequestHandlerTests
    {
        private readonly IQueryRepository _queryRepository = Substitute.For<IQueryRepository>();
        private readonly ILogger<DeleteQueryByIdRequestHandler> _logger = Substitute.For<ILogger<DeleteQueryByIdRequestHandler>>();
        private readonly DeleteQueryByIdRequestHandler _sut;

        public DeleteQueryByIdRequestHandlerTests()
        {
            _sut = new DeleteQueryByIdRequestHandler(_queryRepository, _logger);
        }

        [Fact]
        public async Task Handle_WhenDeletingQuery_ShouldReturnSuccess()
        {
            // Arrange
            var queryId = 1;

            var existingQuery = new QueryEntity
            {
                Id = queryId,
                Status = true,
                Date = DateTime.UtcNow.AddDays(-1),
                PersonId = 1
            };

            _queryRepository.GetQueryAsync(queryId)
                            .Returns(Task.FromResult(existingQuery));

            var request = new DeleteQueryByIdRequest { Id = queryId };

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<DeleteQueryByIdResponse>>()
                 .Which.IsSuccess.Should().BeTrue();

            await _queryRepository.Received(1).GetQueryAsync(queryId);
            await _queryRepository.Received(1).DeleteQueryAsync(queryId);
        }

        [Fact]
        public async Task Handle_WhenDeletingNonExistingQuery_ShouldReturnFailure()
        {
            // Arrange
            var nonExistingQueryId = 123;

            _queryRepository.GetQueryAsync(nonExistingQueryId)
                            .Returns(Task.FromResult<QueryEntity>(null));

            // Act
            var result = await _sut.Handle(new DeleteQueryByIdRequest { Id = nonExistingQueryId }, CancellationToken.None);
        
            // Assert
            result.Should().NotBeNull(); // Ensure result is not null
            result.IsSuccess.Should().BeFalse(); // Ensure operation failed
        }
    }
}
