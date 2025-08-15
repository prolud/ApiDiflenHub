using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("certificates")]
    public class Certificate
    {
        [Key]
        public int Id { get; set; }

        [Column("unity_id")]
        public int UnityId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public Unity Unity { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}