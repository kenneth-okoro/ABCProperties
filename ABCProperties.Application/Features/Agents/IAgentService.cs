using ABCProperties.Domain.Entities;

namespace ABCProperties.Application.Features.Agents
{
    public interface IAgentService
    {
        Task<int> CreateAsync(Agent createAgent);
        Task<Agent> UpdateAsync(Agent updatedAgent);
        Task<int> DeleteAsync(int id);
        Task<Agent> GetByIdAsync(int id);
        Task<List<Agent>> GetAllAsync();
        Task<bool> DoesExistAsync(int id);
    }
}
