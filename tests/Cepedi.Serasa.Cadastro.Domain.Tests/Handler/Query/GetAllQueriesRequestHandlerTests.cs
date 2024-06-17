using Cepedi.Serasa.Cadastro.Shared.Requests.Query;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Query;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Query
{
    public class GetAllQueriesRequestHandlerTests
    {
        private readonly IQueryRepository _queryRepository = Substitute.For<IQueryRepository>();
        private readonly ILogger<GetAllQueriesRequestHandler> _logger = Substitute.For<ILogger<GetAllQueriesRequestHandler>>();
        private readonly GetAllQueriesRequestHandler _sut;

        public GetAllQueriesRequestHandlerTests()
        {
            _sut = new GetAllQueriesRequestHandler(_logger, _queryRepository);
        }

        [Fact]
        public async Task Handle_WhenFetchingAllQueries_ShouldReturnAllQueries()
        {
            // Arrange
            var queries = new List<QueryEntity>
            {
                new QueryEntity
                {
                    Id = 1,
                    Status = true,
                    Date = DateTime.UtcNow.AddDays(-1),
                    PersonId = 1
                },
                new QueryEntity
                {
                    Id = 2,
                    Status = true,
                    Date = DateTime.UtcNow.AddDays(-1),
                    PersonId = 2
                }
            };

            _queryRepository.GetAllQueriesAsync()
                                .Returns(Task.FromResult(queries));

            var request = new GetAllQueriesRequest();

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            result.Value.Should().HaveCount(queries.Count);

            for (int i = 0; i < queries.Count; i++)
            {
                result.Value[i].Id.Should().Be(queries[i].Id);
                result.Value[i].Status.Should().Be(queries[i].Status);
                result.Value[i].Date.Should().Be(queries[i].Date);
                result.Value[i].PersonId.Should().Be(queries[i].PersonId);
            }

            await _queryRepository.Received(1).GetAllQueriesAsync();
        }

        [Fact]
        public async Task Handle_WhenNoQueriesExist_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyQueries = new List<QueryEntity>();

            _queryRepository.GetAllQueriesAsync()
                                .Returns(Task.FromResult(emptyQueries));

            var request = new GetAllQueriesRequest();

            // Act 
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEmpty();

            await _queryRepository.Received(1).GetAllQueriesAsync();
        }
    }
}
