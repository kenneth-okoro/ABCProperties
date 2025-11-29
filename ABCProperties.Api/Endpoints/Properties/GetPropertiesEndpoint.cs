using ABCProperties.Application.Features.Properties.Queries;
using ABCProperties.Application.Models.Responses;
using ABCProperties.Application.Wrappers;
using MediatR;

namespace ABCProperties.Api.Endpoints.Properties
{
    public static class GetPropertiesEndpoint
    {
        public static RouteHandlerBuilder MapGetPropertiesEndpoint(this IEndpointRouteBuilder endpoint)
        {
            return endpoint.MapGet("", async (ISender sender) =>
            {
                var response = await sender.Send(new GetPropertiesQuery());

                if (response.IsSuccessful)
                    return Results.Ok(response);
                return Results.NotFound(response);
            })
                .WithName(nameof(GetPropertiesEndpoint))
                .WithSummary("Gets all Properties")
                .WithDescription("Returns all properties brought in by agents.")
                .Produces<ResponseWrapper<List<PropertyResponse>>>(StatusCodes.Status200OK)
                .Produces<ResponseWrapper<List<PropertyResponse>>>(StatusCodes.Status404NotFound);
        }
    }
}
