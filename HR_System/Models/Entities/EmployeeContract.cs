using HR_System.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_System.Models.Entities
{
    public class EmploymentContract
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }

        [Required]
        public int VacancyId { get; set; }

        [ForeignKey(nameof(VacancyId))]
        public Vacancy? Vacancy { get; set; }

        [Required]
        [MaxLength(50)]
        public WorkType WorkType { get; set; } = WorkType.FullTime; 

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BaseSalary { get; set; } 

        [Required]
        [MaxLength(50)]
        public WageType WageType { get; set; } = WageType.Monthly; 


        public bool CalculateLeaveAmount { get; set; } = true; 

        public bool DeductFromBasicPay { get; set; } = true;  

        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public DateTime? EndDate { get; set; } 

        [Required]
        [MaxLength(50)]
        public bool IsActive { get; set; } = true; 
    }
}