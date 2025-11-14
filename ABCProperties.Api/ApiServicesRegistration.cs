using ABCProperties.Api.Endpoints.Properties;
using UpdatePropertyEndpoint = ABCProperties.Api.Endpoints.Properties.UpdatePropertyEndpoint;

namespace ABCProperties.Api
{
    public static class ApiServicesRegistration
    {
        public static void MapPropertyEndpoints(this IEndpointRouteBuilder endpoint)
        {
            var propertyGroup = endpoint.MapGroup("api/properties")
                .WithTags("Properties");

            propertyGroup.MapCreatePropertyEndpoint(); 
            propertyGroup.MapUpdatePropertyEndpoint();
            propertyGroup.MapDeletePropertyEndpoint();
            propertyGroup.MapGetPropertyByIdEndpoint();
            propertyGroup.MapGetPropertiesEndpoint();
            propertyGroup.MapGetPropertiesForAgentEndpoint();
        }
    }
}
