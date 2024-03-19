namespace ApiDiflenStore.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; }
        public int UserLevel { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
