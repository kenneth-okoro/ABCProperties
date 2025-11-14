using ABCProperties.Application.Features.Properties.Commands;
using ABCProperties.Application.Models.Requests;
using ABCProperties.Application.Models.Responses;
using ABCProperties.Application.Wrappers;
using MediatR;

namespace ABCProperties.Api.Endpoints.Properties
{
    public static class UpdatePropertyEndpoint
    {
        public static RouteHandlerBuilder MapUpdatePropertyEndpoint(this IEndpointRouteBuilder endpoint)
        {
            return endpoint.MapPut("", async (UpdatePropertyRequest updateProperty, ISender sender) =>
            {
                var response = await sender.Send(new UpdatePropertyCommand { UpdateProperty = updateProperty });

                if (response.IsSuccessful)
                    return Results.Ok(response);
                return Results.BadRequest(response);
            })
                .Produces<ResponseWrapper<PropertyResponse>>(StatusCodes.Status200OK)
                .Produces<ResponseWrapper<PropertyResponse>>(StatusCodes.Status404NotFound);
        }
    }
}
