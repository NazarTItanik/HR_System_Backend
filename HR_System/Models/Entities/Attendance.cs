using HR_System.Enums;
using HR_System.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Attendance
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid EmployeeId { get; set; }

    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }

    [Required]
    public DateTime Date { get; set; } 
    public DateTime? ClockIn { get; set; }  // Время прихода

    public DateTime? ClockOut { get; set; } // Время ухода

    public double TotalHoursWorked { get; set; } // Вычисляется на бэкенде: ClockOut - ClockIn
    public AttendanceStatus Status { get; set; } = AttendanceStatus.Pending;
}