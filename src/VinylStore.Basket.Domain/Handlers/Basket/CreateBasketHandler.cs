using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VinylStore.Basket.Domain.Infrastructure.Repositories;
using VinylStore.Basket.Domain.Requests.Basket;
using VinylStore.Basket.Domain.Responses.Basket;

namespace VinylStore.Basket.Domain.Handlers.Basket
{
    public class CreateBasketHandler : IRequestHandler<CreateBasketRequest, BasketExtendedResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _repository;

        public CreateBasketHandler(IBasketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BasketExtendedResponse> Handle(
            CreateBasketRequest request,
            CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Entities.Basket>(request);
            entity.Id = Guid.NewGuid();

            var result = await _repository.AddOrUpdateAsync(entity);
            return _mapper.Map<BasketExtendedResponse>(result);
        }
    }
}