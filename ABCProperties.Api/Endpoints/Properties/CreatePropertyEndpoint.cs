using ABCProperties.Application.Features.Properties.Commands;
using ABCProperties.Application.Models.Requests;
using ABCProperties.Application.Wrappers;
using MediatR;

namespace ABCProperties.Api.Endpoints.Properties
{
    public static class CreatePropertyEndpoint
    {
        public static RouteHandlerBuilder MapCreatePropertyEndpoint(this IEndpointRouteBuilder endpoint)
        {
            return endpoint.MapPost("", async (CreatePropertyRequest createProperty, ISender sender) =>
            {
                var response = await sender.Send(new CreatePropertyCommand { CreateProperty = createProperty });

                if (response.IsSuccessful)
                    return Results.Ok(response);
                return Results.BadRequest(response);
            })
                .Produces<ResponseWrapper<int>>(StatusCodes.Status200OK)
                .Produces<ResponseWrapper<int>>(StatusCodes.Status400BadRequest);
        }
    }
}
