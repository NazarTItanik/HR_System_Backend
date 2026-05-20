using HR_System.Enums;
using System.ComponentModel.DataAnnotations;

public class CreateCandidateDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    [Required]
    public string LastName { get; set; } = string.Empty;

    public int? VacancyId { get; set; }

    public DateTime? DateOfApplication { get; set; }

    [Required]
    public string Email { get; set; } = string.Empty;

    public string? ContactNumber { get; set; }
    public string? Facebook { get; set; }
    public string? Twitter { get; set; }
    public string? LinkedIn { get; set; }
    public string? Keywords { get; set; }
    [Required]
    public IFormFile resumeFile { get; set; }
    public string? Notes { get; set; }
    public CandidateStage Stage { get; set; } = CandidateStage.New;
}