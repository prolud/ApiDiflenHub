namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        public Task AddExperience(int experienceToAdd, int userId);
    }
}