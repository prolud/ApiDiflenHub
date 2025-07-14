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
}