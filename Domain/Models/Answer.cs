using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("answers")]
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Column("alternative_id")]
        public int AlternativeId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        public DateTime Created { get; set; }
    }
}