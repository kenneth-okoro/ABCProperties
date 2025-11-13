using ABCProperties.Application.Models.Responses;
using ABCProperties.Application.Wrappers;
using Mapster;
using MediatR;

namespace ABCProperties.Application.Features.Properties.Queries
{
    public class GetPropertyByIdQuery : IRequest<ResponseWrapper<PropertyResponse>>
    {
        public int Id { get; set; }
    }

    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, ResponseWrapper<PropertyResponse>>
    {
        private readonly IPropertyService _propertyService;

        public GetPropertyByIdQueryHandler(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<ResponseWrapper<PropertyResponse>> Handle(GetPropertyByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var property = await _propertyService.GetByIdAsync(request.Id);

            if (property is not null)
                return ResponseWrapper<PropertyResponse>.Success(data: property
                    .Adapt<PropertyResponse>());

            return ResponseWrapper<PropertyResponse>.Fail(message: "Property does not exist");
        }
    }
}
