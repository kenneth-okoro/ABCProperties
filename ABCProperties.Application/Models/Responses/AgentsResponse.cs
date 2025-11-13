namespace ABCProperties.Application.Models.Responses
{
    public class AgentsResponse
    {
        public required IEnumerable<AgentResponse> Items { get; set; } = Enumerable.Empty<AgentResponse>();
    }
}
