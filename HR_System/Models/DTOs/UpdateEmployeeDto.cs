using System.ComponentModel.DataAnnotations;

namespace HR_System.Models.DTOs
{
    public class UpdateEmployeeDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
    }
}
