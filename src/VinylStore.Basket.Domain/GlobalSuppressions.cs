// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target = "~M:VinylStore.Basket.Domain.Entities.BasketItem.DecreaseQuantity~System.Int32")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target = "~M:VinylStore.Basket.Domain.Entities.BasketItem.IncreaseQuantity~System.Int32")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Basket.Domain.Handlers.Basket.CreateBasketHandler.#ctor(VinylStore.Basket.Domain.Infrastructure.Repositories.IBasketRepository,AutoMapper.IMapper,VinylStore.Basket.Domain.Infrastructure.CatalogEnricher.ICatalogEnricher)")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Basket.Domain.Handlers.Basket.CreateBasketHandler.Handle(VinylStore.Basket.Domain.Requests.Basket.CreateBasketRequest,System.Threading.CancellationToken)~System.Threading.Tasks.Task{VinylStore.Basket.Domain.Responses.Basket.BasketExtendedResponse}")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Basket.Domain.Handlers.Basket.GetBasketHandler.#ctor(VinylStore.Basket.Domain.Infrastructure.Repositories.IBasketRepository,AutoMapper.IMapper,VinylStore.Basket.Domain.Infrastructure.CatalogEnricher.ICatalogEnricher)")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Basket.Domain.Handlers.Basket.GetBasketHandler.Handle(VinylStore.Basket.Domain.Requests.Basket.GetBasketRequest,System.Threading.CancellationToken)~System.Threading.Tasks.Task{VinylStore.Basket.Domain.Responses.Basket.BasketExtendedResponse}")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Basket.Domain.Handlers.Basket.UpdateBasketItemQuantity.#ctor(VinylStore.Basket.Domain.Infrastructure.Repositories.IBasketRepository,AutoMapper.IMapper,VinylStore.Basket.Domain.Infrastructure.CatalogEnricher.ICatalogEnricher)")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Basket.Domain.Handlers.Basket.UpdateBasketItemQuantity.Handle(VinylStore.Basket.Domain.Requests.Basket.UpdateBasketItemQuantityRequest,System.Threading.CancellationToken)~System.Threading.Tasks.Task{VinylStore.Basket.Domain.Responses.Basket.BasketExtendedResponse}")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Basket.Domain.Infrastructure.CatalogEnricher.CatalogEnricher.#ctor(VinylStore.Catalog.API.Client.ICatalogClient)")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Basket.Domain.Infrastructure.CatalogEnricher.CatalogEnricher.EnrichBasketItem(VinylStore.Basket.Domain.Responses.Basket.BasketItemResponse,System.Threading.CancellationToken)~System.Threading.Tasks.Task{VinylStore.Basket.Domain.Responses.Basket.BasketItemResponse}")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target = "~M:VinylStore.Basket.Domain.Infrastructure.Mapper.BasketProfile.#ctor")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target = "~P:VinylStore.Basket.Domain.Requests.Basket.UpdateBasketItemQuantityRequest.IsRemoveOperation")]