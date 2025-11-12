using ABCProperties.Application.Models.Requests;
using ABCProperties.Application.Wrappers;
using ABCProperties.Domain.Entities;
using Mapster;
using MediatR;

namespace ABCProperties.Application.Features.Agents.Commands
{
    public class CreateAgentCommand : IRequest<IResponseWrapper>
    {
        public CreateAgentRequest CreateAgent { get; set; }
    }

    public class CreateAgentCommandHandler : IRequestHandler<CreateAgentCommand, IResponseWrapper>
    {
        private readonly IAgentService _agentService;
        public CreateAgentCommandHandler(IAgentService agentService)
        {
            _agentService = agentService;
        }
        public async Task<IResponseWrapper> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
        {
            var agentId = await _agentService.CreateAsync(request.CreateAgent.Adapt<Agent>());

            return ResponseWrapper<int>.Success(data: agentId, message: "Agent created successfully.");
        }
    }
}
