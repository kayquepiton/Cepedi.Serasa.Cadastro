using Cepedi.Serasa.Cadastro.Shared.Requests.Query;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Query;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Query
{
    public class GetQueryByIdRequestHandlerTests
    {
        private readonly IQueryRepository _queryRepository = Substitute.For<IQueryRepository>();
        private readonly ILogger<GetQueryByIdRequestHandler> _logger = Substitute.For<ILogger<GetQueryByIdRequestHandler>>();
        private readonly GetQueryByIdRequestHandler _sut;

        public GetQueryByIdRequestHandlerTests()
        {
            _sut = new GetQueryByIdRequestHandler(_queryRepository, _logger);
        }

        [Fact]
        public async Task Handle_WhenFetchingExistingQuery_ShouldReturnQuery()
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

            var request = new GetQueryByIdRequest { Id = queryId };

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            result.Value.Id.Should().Be(existingQuery.Id);
            result.Value.Status.Should().Be(existingQuery.Status);
            result.Value.Date.Should().Be(existingQuery.Date);
            result.Value.PersonId.Should().Be(existingQuery.PersonId);

            await _queryRepository.Received(1).GetQueryAsync(queryId);
        }

        [Fact]
        public async Task Handle_WhenFetchingNonExistingQuery_ShouldReturnNull()
        {
            // Arrange
            var nonExistingQueryId = 123;

            _queryRepository.GetQueryAsync(nonExistingQueryId)
                            .Returns(Task.FromResult<QueryEntity>(null));

            var request = new GetQueryByIdRequest { Id = nonExistingQueryId };

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeNull();

            await _queryRepository.Received(1).GetQueryAsync(nonExistingQueryId);
        }
    }
}
