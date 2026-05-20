using HR_System.Models.Entities;

namespace HR_System.Repositories
{
    public class InMemoryCandidateRepository : ICandidateRepository
    {
        // The static list acts as our temporary database table
        private static readonly List<Candidate> _candidates = new List<Candidate>();

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            return await Task.FromResult(_candidates);
        }

        public async Task<Candidate?> GetByIdAsync(Guid id)
        {
            var candidate = _candidates.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(candidate);
        }

        public async Task AddAsync(Candidate candidate)
        {
            candidate.Id = Guid.NewGuid();
            _candidates.Add(candidate);
            await Task.CompletedTask;
        }
    }
}