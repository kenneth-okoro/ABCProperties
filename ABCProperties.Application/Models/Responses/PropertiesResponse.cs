namespace ABCProperties.Application.Models.Responses
{
    public class PropertiesResponse
    {
        public required IEnumerable<PropertyResponse> Items { get; set; } = Enumerable.Empty<PropertyResponse>();
    }
}
