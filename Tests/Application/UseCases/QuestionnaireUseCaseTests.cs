using Application.UseCases;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Tests.Application.UseCases
{
    public class QuestionnaireUseCaseTests
    {
        private readonly Mock<IAlternativeService> _alternativeServiceMock = new();
        private readonly Mock<IAnswerService> _answerServiceMock = new();
        private readonly Mock<IQuestionService> _questionServiceMock = new();
        private readonly QuestionnaireUseCase _useCase;

        public QuestionnaireUseCaseTests()
        {
            _useCase = new QuestionnaireUseCase(_alternativeServiceMock.Object, _answerServiceMock.Object, _questionServiceMock.Object);
        }

        [Fact]
        public async Task VerifyAnswersAsync_ShouldReturnCorrectAnswers_WhenAlternativesExist()
        {
            // Arrange
            var input = new List<AnswerVerifyIn>
            {
                new() { QuestionId = 1, AlternativeId = 10},
                new() { QuestionId = 2, AlternativeId = 20}
            };

            _alternativeServiceMock.Setup(x => x.GetCorrectAlternativeAsync(1))
                .ReturnsAsync(
                    new Alternative
                    {
                        Id = 10,
                        IsCorrect = true
                    }
                );
            _alternativeServiceMock.Setup(x => x.GetCorrectAlternativeAsync(2))
                .ReturnsAsync(
                    new Alternative
                    {
                        Id = 21,
                        IsCorrect = false
                    }
                );

            _answerServiceMock.Setup(x => x.UpsertAnswerAsync(It.IsAny<Answer>()))
                .Returns(Task.CompletedTask);

            _questionServiceMock.Setup(q => q.GetQuestion(It.IsAny<int>()))
                .ReturnsAsync(new Question());

            // Act
            var result = await _useCase.VerifyAnswersAsync(input, "1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result!.Count);
            Assert.True(result[0].IsCorrect);  // primeira correta
            Assert.False(result[1].IsCorrect); // segunda incorreta

            _answerServiceMock.Verify(x => x.UpsertAnswerAsync(It.IsAny<Answer>()), Times.Exactly(2));
        }

        [Fact]
        public async Task VerifyAnswersAsync_ShouldReturnNull_WhenCorrectAlternativeIsNull()
        {
            // Arrange
            var input = new List<AnswerVerifyIn>
        {
            new() { QuestionId = 1, AlternativeId = 10 }
        };

            _alternativeServiceMock.Setup(x => x.GetCorrectAlternativeAsync(1))
                .ReturnsAsync((Alternative?)null);

            // Act
            var result = await _useCase.VerifyAnswersAsync(input, "1");

            // Assert
            Assert.Null(result);
            _answerServiceMock.Verify(x => x.UpsertAnswerAsync(It.IsAny<Answer>()), Times.Never);
        }

        [Fact]
        public async Task VerifyAnswersAsync_ShouldInsertAnswersWithCorrectValues()
        {
            // Arrange
            var input = new List<AnswerVerifyIn>
            {
                new() { QuestionId = 1, AlternativeId = 10 }
            };

            _alternativeServiceMock.Setup(x => x.GetCorrectAlternativeAsync(1))
                .ReturnsAsync(new Alternative());

            Answer? insertedAnswer = null;
            _answerServiceMock.Setup(x => x.UpsertAnswerAsync(It.IsAny<Answer>()))
                .Callback<Answer>(a => insertedAnswer = a)
                .Returns(Task.CompletedTask);

            _questionServiceMock.Setup(q => q.GetQuestion(It.IsAny<int>()))
            .ReturnsAsync(new Question() {Id = 1});

            // Act
            await _useCase.VerifyAnswersAsync(input, "100");

            // Assert
            Assert.NotNull(insertedAnswer);
            Assert.Equal(1, insertedAnswer!.QuestionId);
            Assert.Equal(10, insertedAnswer.AlternativeId);
            Assert.Equal(100, insertedAnswer.UserId);
            Assert.True(insertedAnswer.Created <= DateTime.Now);
        }
    }
}