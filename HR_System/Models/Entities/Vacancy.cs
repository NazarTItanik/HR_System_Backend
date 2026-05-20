using System.ComponentModel.DataAnnotations;

namespace HR_System.Models.Entities
{
    public class Vacancy
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
    }
}