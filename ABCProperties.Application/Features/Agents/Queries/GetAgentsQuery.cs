using ABCProperties.Application.Models.Responses;
using ABCProperties.Application.Wrappers;
using Mapster;
using MediatR;

namespace ABCProperties.Application.Features.Agents.Queries
{
    public class GetAgentsQuery : IRequest<ResponseWrapper<List<AgentResponse>>>
    {
    }

    public class GetAgentsQueryHandler : IRequestHandler<GetAgentsQuery, ResponseWrapper<List<AgentResponse>>>
    {
        private readonly IAgentService _agentService;
        public GetAgentsQueryHandler(IAgentService agentService)
        {
            _agentService = agentService;
        }

        public async Task<ResponseWrapper<List<AgentResponse>>> Handle(GetAgentsQuery request, 
            CancellationToken cancellationToken)
        {
            var agentsEntity = await _agentService.GetAllAsync();

            if (agentsEntity.Count > 0)
            {
                return ResponseWrapper<List<AgentResponse>>.Success(data: agentsEntity.Adapt<List<AgentResponse>>());
            }
            return ResponseWrapper<List<AgentResponse>>.Fail(message: "No agents found.");
        }
    }
}
