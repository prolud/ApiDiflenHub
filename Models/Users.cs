using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDiflenStore.Models
{
    [Table("users")]
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int UserLevel { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
