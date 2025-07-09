using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Certificate
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Unity")]
        [Column("unity_id")]
        public int UnityId { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }

        public DateTime Created { get; set; }
    }
}