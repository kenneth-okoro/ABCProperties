using ABCProperties.Application.Pipelines.Contracts;
using ABCProperties.Application.Wrappers;
using FluentValidation;
using MediatR;

namespace ABCProperties.Application.Pipelines
{
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IValidatable
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators=validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators
                    .Select(vr => vr.ValidateAsync(context, cancellationToken)));

                if (!validationResults.Any(vr => vr.IsValid))
                {
                    var errorMessages = new List<string>();
                    var failures = validationResults
                        .SelectMany(vr => vr.Errors).Where(f => f != null).ToList();

                    foreach (var failure in failures)
                    {
                        errorMessages.Add(failure.ErrorMessage);
                    }

                    return (TResponse)ResponseWrapper.Fail(messages: errorMessages);
                };
            }

            return await next();
        }
    }
}
