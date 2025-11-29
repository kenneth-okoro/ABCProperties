using ABCProperties.Api.Endpoints.Properties;
using ABCProperties.Application;
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

        public static CacheSettings GetCacheSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var cacheSettingsConfig = configuration.GetSection("CacheSettings");
            services.Configure<CacheSettings>(cacheSettingsConfig);

            return cacheSettingsConfig.Get<CacheSettings>();

        }
    }
}

