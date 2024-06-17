using Cepedi.Serasa.Cadastro.Domain.Handlers.Score;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Shared.Responses.Score;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Score
{
    public class DeleteScoreByIdRequestHandlerTests
    {
        private readonly IScoreRepository _scoreRepository = Substitute.For<IScoreRepository>();
        private readonly ILogger<DeleteScoreByIdRequestHandler> _logger = Substitute.For<ILogger<DeleteScoreByIdRequestHandler>>();
        private readonly DeleteScoreByIdRequestHandler _sut;

        public DeleteScoreByIdRequestHandlerTests()
        {
            _sut = new DeleteScoreByIdRequestHandler(_scoreRepository, _logger);
        }

        [Fact]
        public async Task Handle_WhenDeleteScore_ShouldReturnSuccess()
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

            // Act
            var result = await _sut.Handle(new DeleteScoreByIdRequest { Id = scoreId }, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<DeleteScoreByIdResponse>>()
                .Which.IsSuccess.Should().BeTrue();

            // Verify if the method on the repository was called correctly
            await _scoreRepository.Received(1).GetScoreAsync(scoreId);
            await _scoreRepository.Received(1).DeleteScoreAsync(scoreId);
        }

        [Fact]
        public async Task Handle_WhenDeleteNonexistentScore_ShouldReturnFailure()
        {
            // Arrange
            var scoreIdInexistente = 99;

            _scoreRepository.GetScoreAsync(scoreIdInexistente).Returns(Task.FromResult<ScoreEntity>(null));

            // Act
            var result = await _sut.Handle(new DeleteScoreByIdRequest { Id = scoreIdInexistente }, CancellationToken.None);

            // Assert
            result.Should().NotBeNull(); // Verifies that the result is not null
            result.IsSuccess.Should().BeFalse(); // Verifies that the operation failed
        }
    }
}
