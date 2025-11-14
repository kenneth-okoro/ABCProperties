using ABCProperties.Application.Models.Requests;
using ABCProperties.Application.Models.Responses;
using ABCProperties.Application.Wrappers;
using ABCProperties.Domain.Entities;
using Mapster;
using MediatR;

namespace ABCProperties.Application.Features.Properties.Commands
{
    public class UpdatePropertyCommand : IRequest<IResponseWrapper>
    {
        public UpdatePropertyRequest UpdateProperty { get; set; }
    }

    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, IResponseWrapper>
    {
        private readonly IPropertyService _propertyService;

        public UpdatePropertyCommandHandler(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<IResponseWrapper> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var updatedEntity = request.UpdateProperty.Adapt<Property>();
            var updatedProperty = await _propertyService.UpdateAsync(updatedEntity);

            if (updatedProperty is not null)
            {
                return ResponseWrapper<PropertyResponse>.Success(data: updatedProperty.Adapt<PropertyResponse>(),
                    message: "Property updated successfully.");
            }

            return ResponseWrapper<PropertyResponse>.Fail(message: "Property does not exist.");
        }
    }
}
