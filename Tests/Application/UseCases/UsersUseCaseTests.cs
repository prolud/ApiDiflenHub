using Application.UseCases;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using Xunit;

namespace Tests.Application.UseCases
{
    public class UsersUseCaseTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UsersUseCase _useCase;

        public UsersUseCaseTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _useCase = new UsersUseCase(_userServiceMock.Object);
        }

        [Fact]
        public async Task RegisterUser_ShouldHashPasswordAndInsertUser()
        {
            // Arrange
            string email = "user@example.com";
            string username = "user";
            string password = "123456";
            User? insertedUser = null;

            _userServiceMock
                .Setup(x => x.InsertUser(It.IsAny<User>()))
                .Callback<User>(u => insertedUser = u)
                .Returns(Task.CompletedTask);

            // Act
            await _useCase.RegisterUser(email, username, password);

            // Assert
            Assert.NotNull(insertedUser);
            Assert.Equal(email, insertedUser!.Email);
            Assert.Equal(username, insertedUser.Username);
            Assert.NotEqual(password, insertedUser.Password); // senha deve estar criptografada
            Assert.True(BCrypt.Net.BCrypt.Verify(password, insertedUser.Password));
        }

        [Fact]
        public async Task LoginUser_ShouldReturnTrue_WhenPasswordMatches()
        {
            // Arrange
            string email = "user@example.com";
            string plainPassword = "123456";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

            var user = new User
            {
                Email = email,
                Username = "user",
                Password = hashedPassword
            };

            _userServiceMock.Setup(x => x.GetUser(email))
                .ReturnsAsync(user);

            // Act
            var result = await _useCase.LoginUser(email, plainPassword);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task LoginUser_ShouldReturnFalse_WhenPasswordDoesNotMatch()
        {
            // Arrange
            string email = "user@example.com";
            string correctPassword = "correct";
            string wrongPassword = "wrong";

            var user = new User
            {
                Email = email,
                Username = "user",
                Password = BCrypt.Net.BCrypt.HashPassword(correctPassword)
            };

            _userServiceMock.Setup(x => x.GetUser(email))
                .ReturnsAsync(user);

            // Act
            var result = await _useCase.LoginUser(email, wrongPassword);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task LoginUser_ShouldReturnNull_WhenUserNotFound()
        {
            // Arrange
            string email = "nonexistent@example.com";

            _userServiceMock.Setup(x => x.GetUser(email))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _useCase.LoginUser(email, "anyPassword");

            // Assert
            Assert.Null(result);
        }
    }
}