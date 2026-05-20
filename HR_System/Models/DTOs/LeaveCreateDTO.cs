using System.ComponentModel.DataAnnotations;
using HR_System.Enums;

namespace HR_System.Models.DTOs
{
    public class LeaveCreateDTO
    {
        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public LeaveType LeaveType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string? Reason { get; set; }
    }
}