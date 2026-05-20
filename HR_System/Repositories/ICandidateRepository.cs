using HR_System.Models.Entities;

namespace HR_System.Repositories
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllAsync();
        Task<Candidate?> GetByIdAsync(Guid id);
        Task AddAsync(Candidate candidate);
        // You can add UpdateAsync and DeleteAsync later
    }
}
