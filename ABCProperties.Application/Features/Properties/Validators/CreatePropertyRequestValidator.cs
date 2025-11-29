using ABCProperties.Application.Features.Agents;
using ABCProperties.Application.Models.Requests;
using FluentValidation;

namespace ABCProperties.Application.Features.Properties.Validators
{
    public class CreatePropertyRequestValidator : AbstractValidator<CreatePropertyRequest>
    {
        public CreatePropertyRequestValidator(IAgentService agentService)
        {
            RuleFor(request => request.ShortDescription)
                .NotEmpty().WithMessage("Short description is required.");
            RuleFor(request => request.Price)
                .GreaterThan(0.0M).WithMessage("Price must be greater than zero.");
            RuleFor(request => request.AgentId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Agent ID is required.")
                .MustAsync(async (agentId, ct) 
                => await agentService.DoesExistAsync(agentId))
                .WithMessage("Agent does not exist.");
        }
    }
}
