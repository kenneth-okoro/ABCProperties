using ABCProperties.Application.Features.Agents.Commands;
using ABCProperties.Application.Features.Agents.Queries;
using ABCProperties.Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ABCProperties.Api.Controllers
{
    [Route("api/[controller]")]
    public class AgentsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateAgentAsync([FromBody] CreateAgentRequest createAgent)
        {
            var response = await Sender.Send(new CreateAgentCommand { CreateAgent = createAgent });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAgentAsync()
        {
            var response = await Sender.Send(new GetAgentsQuery());

            if (response.IsSuccessful)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgentByIdAsync(int id)
        {
            var response = await Sender.Send(new GetAgentByIdQuery { AgentId = id });

            if (response.IsSuccessful)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAgentAsync([FromBody] UpdateAgentRequest updateAgent)
        {
            var response = await Sender.Send(new UpdateAgentCommand { UpdateAgent = updateAgent });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgentAsync(int id)
        {
            var response = await Sender.Send(new DeleteAgentCommand { AgentId = id });

            if (response.IsSuccessful)
                return Ok(response);

            return BadRequest(response);
        }

    }
}
