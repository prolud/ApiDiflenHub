using Application.UseCases;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Tests.Application.UseCases
{
    public class QuestionnaireUseCaseTests
    {
        private readonly Mock<IAlternativeService> _alternativeServiceMock;
        private readonly Mock<IAnswerService> _answerServiceMock;
        private readonly QuestionnaireUseCase _useCase;

        public QuestionnaireUseCaseTests()
        {
            _alternativeServiceMock = new Mock<IAlternativeService>();
            _answerServiceMock = new Mock<IAnswerService>();
            _useCase = new QuestionnaireUseCase(_alternativeServiceMock.Object, _answerServiceMock.Object);
        }

        [Fact]
        public async Task VerifyAnswersAsync_ShouldReturnCorrectAnswers_WhenAlternativesExist()
        {
            // Arrange
            var input = new List<AnswerVerifyIn>
        {
            new() { QuestionId = 1, AlternativeId = 10, UserId = 100 },
            new() { QuestionId = 2, AlternativeId = 20, UserId = 100 }
        };

            _alternativeServiceMock.Setup(x => x.GetCorrectAlternativeIdAsync(1))
                .ReturnsAsync(10); // correta
            _alternativeServiceMock.Setup(x => x.GetCorrectAlternativeIdAsync(2))
                .ReturnsAsync(21); // incorreta

            _answerServiceMock.Setup(x => x.InsertAnswerAsync(It.IsAny<Answer>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.VerifyAnswersAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result!.Count);
            Assert.True(result[0].IsCorrect);  // primeira correta
            Assert.False(result[1].IsCorrect); // segunda incorreta

            _answerServiceMock.Verify(x => x.InsertAnswerAsync(It.IsAny<Answer>()), Times.Exactly(2));
        }

        [Fact]
        public async Task VerifyAnswersAsync_ShouldReturnNull_WhenCorrectAlternativeIsNull()
        {
            // Arrange
            var input = new List<AnswerVerifyIn>
        {
            new() { QuestionId = 1, AlternativeId = 10, UserId = 100 }
        };

            _alternativeServiceMock.Setup(x => x.GetCorrectAlternativeIdAsync(1))
                .ReturnsAsync((int?)null);

            // Act
            var result = await _useCase.VerifyAnswersAsync(input);

            // Assert
            Assert.Null(result);
            _answerServiceMock.Verify(x => x.InsertAnswerAsync(It.IsAny<Answer>()), Times.Never);
        }

        [Fact]
        public async Task VerifyAnswersAsync_ShouldInsertAnswersWithCorrectValues()
        {
            // Arrange
            var input = new List<AnswerVerifyIn>
        {
            new() { QuestionId = 1, AlternativeId = 10, UserId = 100 }
        };

            _alternativeServiceMock.Setup(x => x.GetCorrectAlternativeIdAsync(1))
                .ReturnsAsync(10);

            Answer? insertedAnswer = null;
            _answerServiceMock.Setup(x => x.InsertAnswerAsync(It.IsAny<Answer>()))
                .Callback<Answer>(a => insertedAnswer = a)
                .Returns(Task.CompletedTask);

            // Act
            await _useCase.VerifyAnswersAsync(input);

            // Assert
            Assert.NotNull(insertedAnswer);
            Assert.Equal(1, insertedAnswer!.QuestionId);
            Assert.Equal(10, insertedAnswer.AlternativeId);
            Assert.Equal(100, insertedAnswer.UserId);
            Assert.True(insertedAnswer.Created <= DateTime.Now);
        }
    }
}