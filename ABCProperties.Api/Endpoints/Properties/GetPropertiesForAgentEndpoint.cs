using ABCProperties.Application.Features.Properties.Queries;
using ABCProperties.Application.Models.Responses;
using ABCProperties.Application.Wrappers;
using MediatR;

namespace ABCProperties.Api.Endpoints.Properties
{
    public static class GetPropertiesForAgentEndpoint
    {
        public static RouteHandlerBuilder MapGetPropertiesForAgentEndpoint(this IEndpointRouteBuilder endpoint)
        {
            return endpoint.MapGet("/agent/{agentId}", async (int agentId, ISender sender) =>
            {
                var response = await sender.Send(new GetPropertiesForAgentQuery { AgentId = agentId });
                if (response.IsSuccessful)
                    return Results.Ok(response);
                return Results.NotFound(response);
            })
                .Produces<ResponseWrapper<List<PropertyResponse>>>(StatusCodes.Status200OK)
                .Produces<ResponseWrapper<List<PropertyResponse>>>(StatusCodes.Status404NotFound);
        }
    }
}
