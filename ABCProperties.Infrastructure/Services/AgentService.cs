using ABCProperties.Application.Features.Agents;
using ABCProperties.Domain.Entities;
using ABCProperties.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ABCProperties.Infrastructure.Services
{
    public class AgentService : IAgentService
    {
        private readonly AppDbContext _context;

        public AgentService(AppDbContext context)
        {
            _context=context;
        }

        public async Task<int> CreateAsync(Agent createAgent)
        {
            await _context.Agents.AddAsync(createAgent);
            await  _context.SaveChangesAsync();
            return createAgent.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var agentEntity = await _context
                .Agents.FirstOrDefaultAsync
                    (agent => agent.Id == id);

            if (agentEntity is not null)
            {
                _context.Agents.Remove(agentEntity);
                await _context.SaveChangesAsync();
                return agentEntity.Id;
            }
            return 0;
        }

        public async Task<bool> DoesExistAsync(int id)
        {
            return await _context.Agents
                .AnyAsync(agent => 
                    agent.Id == id);
        }

        public async Task<List<Agent>> GetAllAsync()
        {
            return await _context.Agents
                .Include(agent => agent.Properties)
                .Select(agent => new Agent
                {
                    Id = agent.Id,
                    FirstName = agent.FirstName,
                    LastName = agent.LastName,
                    PhoneNumber = agent.PhoneNumber,
                    Email = agent.Email,
                    Properties = agent.Properties
                    .Select(property => new Property
                    {
                        Id = property.Id,
                        AgentId = property.AgentId,
                        ShortDescription = property.ShortDescription,
                        LongDescription = property.LongDescription,
                        Price = property.Price,
                        ListingDate = property.ListingDate
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<Agent> GetByIdAsync(int id)
        {
            var agentEntity = await _context
                .Agents.Include(agent => agent.Properties)
                .Select(agent => new Agent
                {
                    Id = agent.Id,
                    FirstName = agent.FirstName,
                    LastName = agent.LastName,
                    PhoneNumber = agent.PhoneNumber,
                    Email = agent.Email,
                    Properties = agent.Properties
                    .Select(property => new Property
                    {
                        Id = property.Id,
                        AgentId = property.AgentId,
                        ShortDescription = property.ShortDescription,
                        LongDescription = property.LongDescription,
                        Price = property.Price,
                        ListingDate = property.ListingDate
                    }).ToList()
                })
                .FirstOrDefaultAsync
                    (agent => agent.Id == id);

            if (agentEntity is not null)
            {
                return agentEntity;
            }
            return null;
        }

        public async Task<Agent> UpdateAsync(Agent updateAgent)
        {
            if (await DoesExistAsync(updateAgent.Id))
            {
                _context.Agents.Update(updateAgent);
                await _context.SaveChangesAsync();
                return updateAgent;
            }
            return null;
        }
    }
}
