using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Score;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Score
{
    public class UpdateScoreRequestHandlerTests
    {
        private readonly IScoreRepository _scoreRepository = Substitute.For<IScoreRepository>();
        private readonly ILogger<UpdateScoreRequestHandler> _logger = Substitute.For<ILogger<UpdateScoreRequestHandler>>();
        private readonly UpdateScoreRequestHandler _sut;

        public UpdateScoreRequestHandlerTests()
        {
            _sut = new UpdateScoreRequestHandler(_scoreRepository, _logger);
        }

        [Fact]
        public async Task Handle_WhenUpdateScore_ShouldReturnSuccess()
        {
            // Arrange
            var request = new UpdateScoreRequest
            {
                Id = 1,
                Score = 800
            };

            var scoreEntity = new ScoreEntity
            {
                Id = request.Id,
                Score = 750
            };

            _scoreRepository.GetScoreAsync(request.Id).Returns(Task.FromResult(scoreEntity));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<UpdateScoreResponse>>()
                    .Which.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(scoreEntity.Id);
            result.Value.Score.Should().Be(request.Score);

            await _scoreRepository.Received(1).GetScoreAsync(request.Id);
            await _scoreRepository.Received(1).UpdateScoreAsync(Arg.Is<ScoreEntity>(
                s => s.Id == request.Id && s.Score == request.Score
            ));
        }

        [Fact]
        public async Task Handle_WhenScoreDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var request = new UpdateScoreRequest
            {
                Id = 1,
                Score = 800
            };

            _scoreRepository.GetScoreAsync(request.Id).Returns(Task.FromResult<ScoreEntity>(null));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<UpdateScoreResponse>>()
                    .Which.IsSuccess.Should().BeFalse();

            result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                    .Which.ErrorResult.Should().Be(RegistrationErrors.InvalScoreIdId);
        }
    }
}
