using ABCProperties.Application.Models.Responses;
using ABCProperties.Application.Wrappers;
using Mapster;
using MediatR;

namespace ABCProperties.Application.Features.Agents.Queries
{
    public class GetAgentByIdQuery : IRequest<ResponseWrapper<AgentResponse>>
    {
        public int AgentId { get; set; }
    }

    public class GetAgentByIdQueryHandler : IRequestHandler<GetAgentByIdQuery, ResponseWrapper<AgentResponse>>
    {
        private readonly IAgentService _agentService;
        public GetAgentByIdQueryHandler(IAgentService agentService)
        {
            _agentService = agentService;
        }

        public async Task<ResponseWrapper<AgentResponse>> Handle(GetAgentByIdQuery request, 
            CancellationToken cancellationToken)
        {
            // Manual Mapping
            //var agent = request.CreateAgent.MapToAgent();

            var agentEntity = await _agentService.GetByIdAsync(request.AgentId);

            if (agentEntity is not null)
            {
                return ResponseWrapper<AgentResponse>.Success(data: agentEntity.Adapt<AgentResponse>());
            }
            return ResponseWrapper<AgentResponse>.Fail(message: "Agent not found.");
        }
    }
}
