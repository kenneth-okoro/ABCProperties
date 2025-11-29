using ABCProperties.Application.Features.Agents;
using ABCProperties.Application.Features.Properties.Commands;
using FluentValidation;

namespace ABCProperties.Application.Features.Properties.Validators
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator(IAgentService agentService)
        {
            RuleFor(command => command.CreateProperty)
                .SetValidator(new CreatePropertyRequestValidator(agentService));
        }
    }
}
