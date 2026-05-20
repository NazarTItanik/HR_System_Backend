using System;

namespace HR_System.Models.Entities
{
    public class JobApplication
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Данные из формы
        public string Name { get; set; } = string.Empty;
        public string JobPosition { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Portfolio { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;

        // В базе мы храним не сам файл, а путь к нему (например: "/uploads/resumes/john_doe.pdf")
        public string? ResumeFilePath { get; set; }

        // Технические поля
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}