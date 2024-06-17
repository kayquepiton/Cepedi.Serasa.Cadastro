using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using Cepedi.Serasa.Cadastro.Shared.Responses.Score;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Handlers.Score;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handlers.Score
{
    public class CreateScoreRequestHandlerTests
    {
        private readonly IScoreRepository _scoreRepository = Substitute.For<IScoreRepository>();
        private readonly IPersonRepository _personRepository = Substitute.For<IPersonRepository>();
        private readonly ILogger<CreateScoreRequestHandler> _logger = Substitute.For<ILogger<CreateScoreRequestHandler>>();
        private readonly CreateScoreRequestHandler _sut;

        public CreateScoreRequestHandlerTests()
        {
            _sut = new CreateScoreRequestHandler(_scoreRepository, _logger, _personRepository);
        }

        [Fact]
        public async Task Handle_WhenCreateScore_ShouldReturnSuccess()
        {
            // Arrange
            var person = new PersonEntity
            {
                Id = 1,
                Name = "João",
                CPF = "12345678901"
            };

            var request = new CreateScoreRequest
            {
                PersonId = person.Id,
                Score = 750
            };

            _personRepository.GetPersonAsync(request.PersonId).Returns(Task.FromResult(person));
            _scoreRepository.GetPersonScoreAsync(request.PersonId).Returns(Task.FromResult<ScoreEntity>(null));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<CreateScoreResponse>>()
                .Which.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();
            result.Value.PersonId.Should().Be(request.PersonId);
            result.Value.Score.Should().Be(request.Score);

            await _personRepository.Received(1).GetPersonAsync(request.PersonId);
            await _scoreRepository.Received(1).GetPersonScoreAsync(request.PersonId);
            await _scoreRepository.Received(1).CreateScoreAsync(Arg.Is<ScoreEntity>(s => s.PersonId == request.PersonId && s.Score == request.Score));
        }

        [Fact]
        public async Task Handle_WhenPersonDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var personId = 999; // Invalid ID that does not exist in the repository
            var request = new CreateScoreRequest
            {
                PersonId = personId,
                Score = 750
            };

            _personRepository.GetPersonAsync(request.PersonId).Returns(Task.FromResult<PersonEntity>(null));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<CreateScoreResponse>>()
                .Which.IsSuccess.Should().BeFalse();

            result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                .Which.ErrorResult.Should().Be(RegistrationErrors.InvalidPersonId);

            await _personRepository.Received(1).GetPersonAsync(request.PersonId);
            await _scoreRepository.DidNotReceive().GetPersonScoreAsync(Arg.Any<int>());
            await _scoreRepository.DidNotReceiveWithAnyArgs().CreateScoreAsync(Arg.Any<ScoreEntity>());
        }

        [Fact]
        public async Task Handle_WhenScoreAlreadyExists_ShouldReturnError()
        {
            // Arrange
            var person = new PersonEntity
            {
                Id = 1,
                Name = "João",
                CPF = "12345678901"
            };

            var scoreEntity = new ScoreEntity
            {
                Id = 1,
                PersonId = person.Id,
                Score = 800
            };

            var request = new CreateScoreRequest
            {
                PersonId = person.Id,
                Score = 750
            };

            _personRepository.GetPersonAsync(request.PersonId).Returns(Task.FromResult(person));
            _scoreRepository.GetPersonScoreAsync(request.PersonId).Returns(Task.FromResult(scoreEntity));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<CreateScoreResponse>>()
                .Which.IsSuccess.Should().BeFalse();

            result.Exception.Should().BeOfType<Shared.Exceptions.AppException>()
                .Which.ErrorResult.Should().Be(RegistrationErrors.ScoreAlreadyExists);

            await _personRepository.Received(1).GetPersonAsync(request.PersonId);
            await _scoreRepository.Received(1).GetPersonScoreAsync(request.PersonId);
            await _scoreRepository.DidNotReceiveWithAnyArgs().CreateScoreAsync(Arg.Any<ScoreEntity>());
        }
    }
}
