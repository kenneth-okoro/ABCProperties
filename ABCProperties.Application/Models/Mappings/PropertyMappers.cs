using ABCProperties.Application.Models.Requests;
using ABCProperties.Application.Models.Responses;
using ABCProperties.Domain.Entities;

namespace ABCProperties.Application.Models.Mappings
{
    public static class PropertyMappers
    {
        public static Property MapToProperty(this CreatePropertyRequest createPropertyRequest)
        {
            return new Property
            {
                AgentId = createPropertyRequest.AgentId,
                Price = createPropertyRequest.Price,
                ShortDescription = createPropertyRequest.ShortDescription,
                LongDescription = createPropertyRequest.LongDescription,
                ListingDate = DateTime.UtcNow
            };
        }

        public static Property MapToProperty(this UpdatePropertyRequest updatePropertyRequest)
        {
            return new Property
            {
                Id = 1,
                AgentId =updatePropertyRequest.AgentId,
                Price = updatePropertyRequest.Price,
                ShortDescription = updatePropertyRequest.ShortDescription,
                LongDescription = updatePropertyRequest.LongDescription
            };
        }

        public static PropertyResponse MapToPropertyResponse(this Property agentModel)
        {
            return new PropertyResponse
            {
                Id = 1,
                AgentId = agentModel.Id,
                Price = agentModel.Price,
                ShortDescription = agentModel.ShortDescription,
                LongDescription = agentModel.LongDescription
            };
        }


        public static PropertiesResponse MapToPropertiesResponse(this IEnumerable<Property> properties)
        {
            return new PropertiesResponse
            {
                Items = properties.Select(MapToPropertyResponse)
            };
        }
    }
}
