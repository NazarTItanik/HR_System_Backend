using System;
using HR_System.Enums;
using System.ComponentModel.DataAnnotations;


namespace HR_System.Models.Entities
{
    public class Candidate
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? MiddleName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        // Made nullable in case of general resume drops
        public int? VacancyId { get; set; }

        // Navigation property (Assuming you have a Vacancy class, uncomment later if needed)
        // public Vacancy? Vacancy { get; set; }

        [MaxLength(500)]
        public string? resumeFile { get; set; }

        public DateTime DateOfApplication { get; set; }

        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ContactNumber { get; set; }

        [MaxLength(255)]
        public string? Facebook { get; set; }
        [MaxLength(255)]
        public string? Twitter { get; set; }
        [MaxLength(255)]
        public string? LinkedIn { get; set; }

        [MaxLength(500)]
        public string? Keywords { get; set; }

        public string? Notes { get; set; }

        [MaxLength(50)]
        public CandidateStage Stage { get; set; } = CandidateStage.New;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}