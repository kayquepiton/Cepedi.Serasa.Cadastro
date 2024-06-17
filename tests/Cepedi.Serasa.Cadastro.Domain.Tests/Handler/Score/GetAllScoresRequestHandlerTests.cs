using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Score;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Score
{
    public class GetAllScoresRequestHandlerTests
    {
        private readonly IScoreRepository _scoreRepository = Substitute.For<IScoreRepository>();
        private readonly ILogger<GetAllScoresRequestHandler> _logger = Substitute.For<ILogger<GetAllScoresRequestHandler>>();
        private readonly GetAllScoresRequestHandler _sut;

        public GetAllScoresRequestHandlerTests()
        {
            _sut = new GetAllScoresRequestHandler(_logger, _scoreRepository);
        }

        [Fact]
        public async Task Handle_WhenGetAllScores_ShouldReturnListOfScores()
        {
            // Arrange
            var scores = new List<ScoreEntity>
            {
                new ScoreEntity { Id = 1, PersonId = 1, Score = 750 },
                new ScoreEntity { Id = 2, PersonId = 2, Score = 800 }
            };

            _scoreRepository.GetAllScoresAsync().Returns(Task.FromResult(scores));

            var request = new GetAllScoresRequest();

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            // Verify if the lists have the same number of elements
            result.Value.Should().HaveCount(scores.Count);

            // Verify if each element in the result list corresponds to the corresponding element in the original list
            for (int i = 0; i < scores.Count; i++)
            {
                result.Value[i].Id.Should().Be(scores[i].Id);
                result.Value[i].PersonId.Should().Be(scores[i].PersonId);
                result.Value[i].Score.Should().Be(scores[i].Score);
            }

            // Verify if the method in the repository was called correctly
            await _scoreRepository.Received(1).GetAllScoresAsync();
        }

        [Fact]
        public async Task Handle_WhenNoScoresExist_ShouldReturnNull()
        {
            // Arrange
            _scoreRepository.GetAllScoresAsync().Returns(Task.FromResult<List<ScoreEntity>>(null));

            var request = new GetAllScoresRequest();

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeNull();

            // Verify if the method in the repository was called correctly
            await _scoreRepository.Received(1).GetAllScoresAsync();
        }
    }
}
