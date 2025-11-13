using ABCProperties.Application.Wrappers;
using MediatR;

namespace ABCProperties.Application.Features.Properties.Commands
{
    public class DeletePropertyCommand : IRequest<IResponseWrapper>
    {
        public int Id { get; set; }
    }

    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, IResponseWrapper>
    {
        private readonly IPropertyService _propertyService;

        public DeletePropertyCommandHandler(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<IResponseWrapper> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var propertyId = await _propertyService.DeleteAsync(request.Id);

            if (propertyId > 0)
                return ResponseWrapper<int>.Success(data: propertyId, message: "Property Deleted Successfully");

            return ResponseWrapper<int>.Fail(message: "Property does not exist.");
        }
    }
}
