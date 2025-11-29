using ABCProperties.Application.Models.Mappings;
using ABCProperties.Application.Models.Requests;
using ABCProperties.Application.Pipelines.Contracts;
using ABCProperties.Application.Wrappers;
using ABCProperties.Domain.Entities;
using Mapster;
using MediatR;

namespace ABCProperties.Application.Features.Properties.Commands
{
    public class CreatePropertyCommand : IRequest<IResponseWrapper>, IValidatable
    {
        public CreatePropertyRequest CreateProperty { get; set; }
    }

    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, IResponseWrapper>
    {
        private readonly IPropertyService _propertyService;

        public CreatePropertyCommandHandler(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<IResponseWrapper> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            // Manual Mapping
            //var property = request.CreateProperty.MapToProperty();

            // Mapster
            //var config = new TypeAdapterConfig();
            //config.ForType<CreatePropertyRequest, Property>()
            //      .Map(dest => dest.ListingDate, src => DateTime.UtcNow);

            var propertyEntity = request.CreateProperty.Adapt<Property>();

            var propertyId = await _propertyService.CreateAsync(propertyEntity);

            return ResponseWrapper<int>.Success(data: propertyId, message: "Property Created Successfully");
        }
    }
}
