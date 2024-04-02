using ApiDiflenStore.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDiflenStore.Db.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int IdUsers { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRolesEnum.IdEnum IdUserRoles { get; set; }
        public bool IsLogged { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
