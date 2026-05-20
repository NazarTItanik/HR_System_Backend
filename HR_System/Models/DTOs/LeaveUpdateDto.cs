using System.ComponentModel.DataAnnotations;

namespace HR_System.Models.DTOs
{
    public class LeaveUpdateDto : LeaveCreateDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}
