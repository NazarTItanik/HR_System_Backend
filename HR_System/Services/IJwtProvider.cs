using HR_System.Models.Entities;

namespace HR_System.Providers
{
    public interface IJwtProvider
    {
        string GenerateToken(Employee user);
    }
}