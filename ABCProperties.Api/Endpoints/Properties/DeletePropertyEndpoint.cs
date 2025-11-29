using ABCProperties.Application.Features.Properties.Commands;
using ABCProperties.Application.Wrappers;
using MediatR;

namespace ABCProperties.Api.Endpoints.Properties
{
    public static class DeletePropertyEndpoint
    {
        public static RouteHandlerBuilder MapDeletePropertyEndpoint(this IEndpointRouteBuilder endpoint)
        {
            return endpoint.MapDelete("", async (int id, ISender sender) =>
            {
                var response = await sender.Send(new DeletePropertyCommand { Id = id });

                if (response.IsSuccessful)
                    return Results.Ok(response);
                return Results.NotFound(response);
            })
                .WithName(nameof(DeletePropertyEndpoint))
                .WithSummary("Deletes a new property")
                .WithDescription("Deletes a new property with the provided id.")
                .Produces<ResponseWrapper<int>>(StatusCodes.Status200OK)
                .Produces<ResponseWrapper<int>>(StatusCodes.Status404NotFound);
        }
    }
}
