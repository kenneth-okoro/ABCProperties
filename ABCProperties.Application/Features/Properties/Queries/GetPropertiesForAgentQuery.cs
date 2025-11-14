using ABCProperties.Application.Features.Agents;
using ABCProperties.Application.Models.Responses;
using ABCProperties.Application.Wrappers;
using Mapster;
using MediatR;

namespace ABCProperties.Application.Features.Properties.Queries
{
    public class GetPropertiesForAgentQuery : IRequest<ResponseWrapper<List<PropertyResponse>>>
    {
        public int AgentId { get; set; }
    }

    public class GetPropertiesForAgentQueryHandler : IRequestHandler<GetPropertiesForAgentQuery, 
        ResponseWrapper<List<PropertyResponse>>>
    {
        private readonly IPropertyService _propertyService;
        private readonly IAgentService _agentService;

        public GetPropertiesForAgentQueryHandler(IPropertyService propertyService, IAgentService agentService)
        {
            _propertyService=propertyService;
            _agentService=agentService;
        }

        public async Task<ResponseWrapper<List<PropertyResponse>>> Handle(GetPropertiesForAgentQuery request, 
            CancellationToken cancellationToken)
        {
            var agent = await _agentService.GetByIdAsync(request.AgentId);

            if (agent is null)
                return ResponseWrapper<List<PropertyResponse>>.Fail(
                    message: "Agent not found."
                );

            var properties = await _propertyService.GetByAgentIdAsync(request.AgentId);

            if (properties.Count == 0)
                return ResponseWrapper<List<PropertyResponse>>.Fail(
                    message: $"No properties were found for {agent.FirstName} {agent.LastName}."
                );

            return ResponseWrapper<List<PropertyResponse>>.Success(
                data: properties.Adapt<List<PropertyResponse>>(),
                message: "Properties retrieved successfully."
            );
        }
    }
}
