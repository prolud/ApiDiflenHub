using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class Categorie
    {
        [Key]
        public int IdCategorie { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
