namespace HR_System.Models.DTOs
{
    using HR_System.Enums;
    using System.ComponentModel.DataAnnotations;

    public class ContractCreateDto
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Vacancy ID is required.")]
        public int VacancyId { get; set; }

        [Required]
        [Range(0.01, 1000000.00, ErrorMessage = "Salary must be between 0.01 and 1,000,000.")]
        public decimal BaseSalary { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Work Type is required.")]
        public WorkType WorkType { get; set; }

        [Required(ErrorMessage = "Wage Type is required.")]
        public WageType WageType { get; set; }
    }
}
