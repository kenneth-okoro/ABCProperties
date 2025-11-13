using ABCProperties.Domain.Entities;

namespace ABCProperties.Application.Features.Properties
{
    public interface IPropertyService
    {
        Task<int> CreateAsync(Property createProperty);
        Task<Property> UpdateAsync(Property updateProperty);
        Task<int> DeleteAsync(int id);
        Task<Property> GetByIdAsync(int id);
        Task<List<Property>> GetAllAsync();
        Task<bool> DoesExistsAsync(int id);
        Task<List<Property>> GetByAgentIdAsync(int agentId);
    }
}
