namespace HR_System.Models.DTOs
{
    public class GenerateCustomPayslipsDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<Guid>? EmployeeIds { get; set; }
    }
}
