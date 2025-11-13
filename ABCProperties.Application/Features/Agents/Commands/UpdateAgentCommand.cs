using ABCProperties.Application.Models.Mappings;
using ABCProperties.Application.Models.Requests;
using ABCProperties.Application.Models.Responses;
using ABCProperties.Application.Wrappers;
using ABCProperties.Domain.Entities;
using Mapster;
using MediatR;

namespace ABCProperties.Application.Features.Agents.Commands
{
    public class UpdateAgentCommand : IRequest<IResponseWrapper>
    {
        public UpdateAgentRequest UpdateAgent { get; set; }
    }

    public class UpdateAgentCommandHandler : IRequestHandler<UpdateAgentCommand, IResponseWrapper>
    {
        private readonly IAgentService _agentService;
        public UpdateAgentCommandHandler(IAgentService agentService)
        {
            _agentService = agentService;
        }
        public async Task<IResponseWrapper> Handle(UpdateAgentCommand request, CancellationToken cancellationToken)
        {
            // Manual Mapping
            //var agent = request.UpdateAgent.MapToAgent();

            var updatedAgent = await _agentService.UpdateAsync(request.UpdateAgent.Adapt<Agent>());

            if (updatedAgent is not null)
            {
                return ResponseWrapper<AgentResponse>.Success(data: updatedAgent.Adapt<AgentResponse>(), 
                    message: "Agent updated successfully.");
            }
            return ResponseWrapper<AgentResponse>.Fail(message: "Agent not found.");
        }
    }
}
