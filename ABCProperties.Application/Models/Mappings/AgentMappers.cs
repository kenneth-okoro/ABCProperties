using ABCProperties.Application.Models.Requests;
using ABCProperties.Application.Models.Responses;
using ABCProperties.Domain.Entities;

namespace ABCProperties.Application.Models.Mappings
{
    public static class AgentMappers
    {
        public static Agent MapToAgent(this CreateAgentRequest createAgentRequest)
        {
            return new Agent
            {
                FirstName = createAgentRequest.FirstName,
                LastName = createAgentRequest.LastName,
                Email = createAgentRequest.Email,
                PhoneNumber = createAgentRequest.PhoneNumber
            };
        }

        public static Agent MapToAgent(this UpdateAgentRequest updateAgentRequest)
        {
            return new Agent
            {
                Id = updateAgentRequest.Id,
                FirstName = updateAgentRequest.FirstName,
                LastName = updateAgentRequest.LastName,
                Email = updateAgentRequest.Email,
                PhoneNumber = updateAgentRequest.PhoneNumber
            };
        }

        public static AgentResponse MapToAgentResponse(this Agent agentModel)
        {
            return new AgentResponse
            {
                Id = agentModel.Id,
                FirstName = agentModel.FirstName,
                LastName = agentModel.LastName,
                Email = agentModel.Email,
                PhoneNumber = agentModel.PhoneNumber,
                Properties = agentModel.Properties
                    .Select(p => p.MapToPropertyResponse()).ToList()
            };
        }

        public static AgentsResponse MapToAgentsResponse(this IEnumerable<Agent> agents)
        {
            return new AgentsResponse
            {
                Items = agents.Select(MapToAgentResponse)
            };
        }

    }
}
