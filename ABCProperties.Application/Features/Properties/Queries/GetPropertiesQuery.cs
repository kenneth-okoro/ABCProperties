using ABCProperties.Application.Models.Responses;
using ABCProperties.Application.Wrappers;
using Mapster;
using MediatR;

namespace ABCProperties.Application.Features.Properties.Queries
{
    public class GetPropertiesQuery : IRequest<ResponseWrapper<List<PropertyResponse>>>
    {
    }

    public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, ResponseWrapper<List<PropertyResponse>>>
    {
        private readonly IPropertyService _propertyService;
        public GetPropertiesQueryHandler(IPropertyService propertyService)
        {
            _propertyService=propertyService;
        }

        public async Task<ResponseWrapper<List<PropertyResponse>>> Handle(GetPropertiesQuery request, 
            CancellationToken cancellationToken)
        {
            var properties = await _propertyService.GetAllAsync();

            if (properties.Count > 0)
                return ResponseWrapper<List<PropertyResponse>>.Success(data: properties
                    .Adapt<List<PropertyResponse>>(), message: "Properties retrieved successfully.");

            return ResponseWrapper<List<PropertyResponse>>.Fail(message: "No properties were found.");
        }
    }
}
