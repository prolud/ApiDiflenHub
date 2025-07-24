using Application.UseCases;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Tests.Application.UseCases
{
    public class LessonUseCaseTests
    {
        private readonly Mock<ILessonService> _lessonService = new();
        private readonly Mock<IUnityService> _unityService = new();
        private readonly Mock<QuestionnaireUseCase> _questionnaireUseCase = new();
        private readonly LessonUseCase _useCase;

        public LessonUseCaseTests()
        {
            _useCase = new LessonUseCase(_lessonService.Object, _unityService.Object, _questionnaireUseCase.Object);
        }

        [Fact]
        public async Task GetLessonsByUnityName_ReturnNull()
        {
            _unityService
                .Setup(x => x.GetUnityByName(It.IsAny<string>()))
                .ReturnsAsync((Unity?)null);

            List<LessonDtoOut> lessons = await _useCase.GetLessonsByUnityName(It.IsAny<string>());
            Assert.Equal(lessons, []);
        }

        [Fact]
        public async Task GetLessonsByUnityName_Success()
        {
            _lessonService
                .Setup(m => m.GetLessonsFromUnityId(It.IsAny<int>()))
                .ReturnsAsync(
                    [
                        new()
                        {
                            Id = 1,
                            Description = "Description",
                            Questions = [],
                            Title = "Title",
                            UnityId = 1,
                            VideoUrl = "VideoUrl",
                        }
                    ]
                );

            _unityService
                .Setup(x => x.GetUnityByName(It.IsAny<string>()))
                .ReturnsAsync(new Unity());

            var expectedReturn = new List<LessonDtoOut>()
            {
                new()
                {
                    Id = 1,
                    Description = "Description",
                    Title = "Title",
                    VideoUrl = "VideoUrl",
                }
            };

            List<LessonDtoOut> lessons = await _useCase.GetLessonsByUnityName(It.IsAny<string>());

            Assert.Equal(lessons.Count, expectedReturn.Count);
            for (var i = 0; i < lessons.Count; i++)
            {
                Assert.Equal(lessons[i].Title, expectedReturn[i].Title);
                Assert.Equal(lessons[i].Description, expectedReturn[i].Description);
                Assert.Equal(lessons[i].Title, expectedReturn[i].Title);
                Assert.Equal(lessons[i].VideoUrl, expectedReturn[i].VideoUrl);
            }
        }

        [Fact]
        public async Task GetLesson_ReturnNull()
        {
            _unityService
                .Setup(x => x.GetUnityByName(It.IsAny<string>()))
                .ReturnsAsync((Unity?)null);

            Lesson? lesson = await _useCase.GetLesson(It.IsAny<string>(), It.IsAny<int>());
            Assert.Null(lesson);
        }

        [Fact]
        public async Task GetLesson_Success()
        {
            _unityService
                .Setup(x => x.GetUnityByName(It.IsAny<string>()))
                .ReturnsAsync(new Unity());

            Lesson? lesson = await _useCase.GetLesson(It.IsAny<string>(), It.IsAny<int>());
        }
    }
}