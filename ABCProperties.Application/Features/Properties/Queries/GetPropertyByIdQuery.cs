using ABCProperties.Application.Models.Responses;
using ABCProperties.Application.Pipelines.Contracts;
using ABCProperties.Application.Wrappers;
using Mapster;
using MediatR;

namespace ABCProperties.Application.Features.Properties.Queries
{
    public class GetPropertyByIdQuery : IRequest<ResponseWrapper<PropertyResponse>>, ICacheable
    {
        public int Id { get; set; }
        public string CacheKey { get; set; }
        public bool IsBypassCache { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public GetPropertyByIdQuery()
        {
            CacheKey = $"{nameof(GetPropertyByIdQuery)}:{Id}";
            IsBypassCache = false;
        }
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
                    .Adapt<PropertyResponse>(), message: "Property retrieved successfully.");

            return ResponseWrapper<PropertyResponse>.Fail(message: "Property does not exist");
        }
    }
}
