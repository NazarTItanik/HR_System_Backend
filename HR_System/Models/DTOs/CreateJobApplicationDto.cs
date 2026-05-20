using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HR_System.Models.DTOs
{
    public class CreateJobApplicationDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a job position")]
        public string JobPosition { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; } = string.Empty;
        public string? Portfolio { get; set; }

        // Для приема файла с фронтенда используется IFormFile.
        // Пока мы не настроили сохранение файлов, можно оставить это поле опциональным
        public IFormFile? ResumeFile { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; } = string.Empty;
    }
}