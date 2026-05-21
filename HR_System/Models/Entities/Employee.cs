using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR_System.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // --- Identity & Authentication ---
        public string Role { get; set; } = "Employee";

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        // --- Personal Data ---
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ContactNumber { get; set; } = string.Empty;

        public DateTime HireDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // --- Navigation Properties (Relationships mapped by Entity Framework) ---
        public ICollection<EmploymentContract> Contracts { get; set; } = new List<EmploymentContract>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
    }
}