using Cepedi.Serasa.Cadastro.Domain.Handlers.Score;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Score
{
    public class GetScoreByIdRequestHandlerTests
    {
        private readonly IScoreRepository _scoreRepository = Substitute.For<IScoreRepository>();
        private readonly ILogger<GetScoreByIdRequestHandler> _logger = Substitute.For<ILogger<GetScoreByIdRequestHandler>>();
        private readonly GetScoreByIdRequestHandler _sut;

        public GetScoreByIdRequestHandlerTests()
        {
            _sut = new GetScoreByIdRequestHandler(_scoreRepository, _logger);
        }

        [Fact]
        public async Task Handle_WhenGetScoreExisting_ShouldReturnScore()
        {
            // Arrange
            var scoreId = 1;

            var scoreExistente = new ScoreEntity
            {
                Id = scoreId,
                PersonId = 1,
                Score = 750
            };

            _scoreRepository.GetScoreAsync(scoreId).Returns(Task.FromResult(scoreExistente));

            var request = new GetScoreByIdRequest { Id = scoreId };

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();

            result.Value.Id.Should().Be(scoreExistente.Id);
            result.Value.PersonId.Should().Be(scoreExistente.PersonId);
            result.Value.Score.Should().Be(scoreExistente.Score);

            // Verify if the method in the repository was called correctly
            await _scoreRepository.Received(1).GetScoreAsync(scoreId);
        }

        [Fact]
        public async Task Handle_WhenGetNonExistingScore_ShouldReturnNull()
        {
            // Arrange
            var scoreIdInexistente = 99;

            _scoreRepository.GetScoreAsync(scoreIdInexistente).Returns(Task.FromResult<ScoreEntity>(null));

            var request = new GetScoreByIdRequest { Id = scoreIdInexistente };

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeNull();

            // Verify if the method in the repository was called correctly
            await _scoreRepository.Received(1).GetScoreAsync(scoreIdInexistente);
        }
    }
}
