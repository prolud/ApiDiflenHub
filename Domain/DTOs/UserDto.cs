using System.Diagnostics.Contracts;

namespace Domain.DTOs
{
    public class LoginDtoIn
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginDtoOut
    {
        public bool IsLogged { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public DateTime? ExpiresIn { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class RegisterDtoIn
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class ProfileDtoOut
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public long Experience { get; set; }
        public int Level { get; set; }
        public string ProfilePic { get; set; } = string.Empty;
        public float LevelPercentage { get; set; }
    }
}