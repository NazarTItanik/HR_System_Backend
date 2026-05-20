using HR_System.Enums;
using HR_System.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Leave
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid EmployeeId { get; set; }

    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }

    [Required]
    public LeaveType LeaveType { get; set; } // Use the Enum

    [Required]
    public bool IsPaid { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal TotalDays { get; set; }

    public string? Reason { get; set; }

    [Required]
    public LeaveStatus Status { get; set; } = LeaveStatus.Pending; 
}