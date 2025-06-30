using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Models
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}
