using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Domain.Requests.Basket;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStore.Basket.Domain.Handlers.Basket
{
    public class GetBasketHandler : IRequestHandler<GetBasketRequest, BasketExtendedResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _repository;

        public GetBasketHandler(IBasketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BasketExtendedResponse> Handle(GetBasketRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(request.Id);
            return _mapper.Map<BasketExtendedResponse>(result);
        }
    }
}