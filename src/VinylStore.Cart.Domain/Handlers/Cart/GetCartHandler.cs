using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VinylStore.Cart.Domain.Infrastructure.Repositories;
using VinylStore.Cart.Domain.Infrastructure.Services;
using VinylStore.Cart.Domain.Requests.Cart;
using VinylStore.Cart.Domain.Responses.Cart;

namespace VinylStore.Cart.Domain.Handlers.Cart
{
    public class GetCartHandler : IRequestHandler<GetCartRequest, CartExtendedResponse>
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;
        private readonly ICartRepository _repository;

        public GetCartHandler(
            ICartRepository repository,
            IMapper mapper,
            ICatalogService catalogService)
        {
            _repository = repository;
            _mapper = mapper;
            _catalogService = catalogService;
        }

        public async Task<CartExtendedResponse> Handle(GetCartRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(request.Id);
            var extendedResponse = _mapper.Map<CartExtendedResponse>(result);

            var tasks = extendedResponse.Items
                .Select(async x => await _catalogService.EnrichCartItem(x, cancellationToken));

            extendedResponse.Items = await Task.WhenAll(tasks);
            return extendedResponse;
        }
    }
}