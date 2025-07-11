using Application.UseCases;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Tests.Application.UseCases
{
    public class UnityUseCaseTests
    {
        private readonly Mock<IUnityService> _unityServiceMock;
        private readonly UnityUseCase _useCase;

        public UnityUseCaseTests()
        {
            _unityServiceMock = new Mock<IUnityService>();
            _useCase = new UnityUseCase(_unityServiceMock.Object);
        }

        [Fact]
        public async Task GetUnities_ShouldReturnMappedList_WhenServiceReturnsData()
        {
            // Arrange
            var dbUnities = new List<Unity>
            {
                new() { Id = 1, Name = "Unity 1", Description = "Desc 1" },
                new() { Id = 2, Name = "Unity 2", Description = "Desc 2" }
            };

            _unityServiceMock.Setup(x => x.GetUnities())
                .ReturnsAsync(dbUnities);

            // Act
            var result = await _useCase.GetUnities();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Unity 1", result[0].Name);
            Assert.Equal("Unity 2", result[1].Name);
        }

        [Fact]
        public async Task GetUnities_ShouldReturnEmptyList_WhenServiceReturnsEmpty()
        {
            // Arrange
            _unityServiceMock.Setup(x => x.GetUnities())
                .ReturnsAsync(new List<Unity>());

            // Act
            var result = await _useCase.GetUnities();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetUnityFromName_ShouldReturnMappedDto_WhenFound()
        {
            // Arrange
            var unity = new Unity { Id = 5, Name = "Test", Description = "Some description" };

            _unityServiceMock.Setup(x => x.GetUnityByName("Test"))
                .ReturnsAsync(unity);

            // Act
            var result = await _useCase.GetUnityFromName("Test");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result!.Id);
            Assert.Equal("Test", result.Name);
            Assert.Equal("Some description", result.Description);
        }

        [Fact]
        public async Task GetUnityFromName_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            _unityServiceMock.Setup(x => x.GetUnityByName("Invalid"))
                .ReturnsAsync((Unity?)null);

            // Act
            var result = await _useCase.GetUnityFromName("Invalid");

            // Assert
            Assert.Null(result);
        }
    }
}