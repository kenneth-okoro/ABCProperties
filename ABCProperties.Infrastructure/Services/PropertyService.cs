using ABCProperties.Application.Features.Properties;
using ABCProperties.Domain.Entities;
using ABCProperties.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ABCProperties.Infrastructure.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly AppDbContext _context;

        public PropertyService(AppDbContext context)
        {
            _context=context;
        }

        public async Task<int> CreateAsync(Property newProperty)
        {
            newProperty.ListingDate = DateTime.UtcNow;
            await _context.Properties.AddAsync(newProperty);
            await _context.SaveChangesAsync();

            return newProperty.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var property = await _context.Properties
                .FirstOrDefaultAsync(x => x.Id == id);

            if (property is not null)
            {
                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();

                return property.Id;
            }

            return 0;
        }

        public async Task<bool> DoesExistsAsync(int id)
        {
            return await _context.Properties
                .AnyAsync(x => x.Id == id);
        }

        public async Task<List<Property>> GetAllAsync()
        {
            return await _context.Properties.ToListAsync();
        }

        public async Task<List<Property>> GetByAgentIdAsync(int agentId)
        {
            return await _context.Properties
                .Where(x => x.AgentId == agentId)
                .ToListAsync();
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            var property = await _context.Properties
                .FirstOrDefaultAsync(x => x.Id == id);

            if (property is not null)
                return property;

            return null;
        }

        public async Task<Property> UpdateAsync(Property updateProperty)
        {
            if (await DoesExistsAsync(updateProperty.Id))
            {
                _context.Properties.Update(updateProperty);
                await _context.SaveChangesAsync();
                return updateProperty;
            }

            return null;
        }
    }
}
