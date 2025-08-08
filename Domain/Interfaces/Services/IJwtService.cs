using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IJwtService
    {
        public string GenerateBearerToken(User user);
        public DateTime GetExpirationDate();
    }
}