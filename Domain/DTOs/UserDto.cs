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
        private readonly int LevelMultiplier = 75;
        
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public long Experience { get; set; }
        public string ProfilePic { get; set; } = string.Empty;
        public int RequiredExperienceToNextLevel => CalculateRequiredNextLevel(Experience);
        public int Level => CalculateLevel(Experience);
        public float LevelPercentage => CalculateLevelPercentage(Experience);

        private int CalculateRequiredNextLevel(long experience)
        {
            int requiredNextLevelValue = 1000;

            while (experience >= requiredNextLevelValue)
            {
                requiredNextLevelValue += requiredNextLevelValue + LevelMultiplier;
            }

            return requiredNextLevelValue;
        }

        private int CalculateLevel(long experience)
        {
            int level = 1;
            int requiredNextLevelValue = 1000;

            while (experience - requiredNextLevelValue >= 0)
            {
                level++;
                experience -= requiredNextLevelValue;
                requiredNextLevelValue += LevelMultiplier;
            }

            return level;
        }

        private float CalculateLevelPercentage(long experience)
        {
            int requiredNextLevelValue = 1000;

            while (experience - requiredNextLevelValue >= 0)
            {
                experience -= requiredNextLevelValue;
                requiredNextLevelValue += LevelMultiplier;
            }

            if (experience == 0) return 0;

            return (100 / (float)requiredNextLevelValue) * experience;
        }
    }

}