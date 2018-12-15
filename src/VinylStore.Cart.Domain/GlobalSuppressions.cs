// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target = "~M:VinylStore.Cart.Domain.Entities.CartItem.DecreaseQuantity~System.Int32")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target = "~M:VinylStore.Cart.Domain.Entities.CartItem.IncreaseQuantity~System.Int32")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Cart.Domain.Handlers.Cart.CreateCartHandler.#ctor(VinylStore.Cart.Domain.Infrastructure.Repositories.ICartRepository,AutoMapper.IMapper,VinylStore.Cart.Domain.Infrastructure.CatalogEnricher.ICatalogEnricher)")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Cart.Domain.Handlers.Cart.CreateCartHandler.Handle(VinylStore.Cart.Domain.Requests.Cart.CreateCartRequest,System.Threading.CancellationToken)~System.Threading.Tasks.Task{VinylStore.Cart.Domain.Responses.Cart.CartExtendedResponse}")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Cart.Domain.Handlers.Cart.GetCartHandler.#ctor(VinylStore.Cart.Domain.Infrastructure.Repositories.ICartRepository,AutoMapper.IMapper,VinylStore.Cart.Domain.Infrastructure.CatalogEnricher.ICatalogEnricher)")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Cart.Domain.Handlers.Cart.GetCartHandler.Handle(VinylStore.Cart.Domain.Requests.Cart.GetCartRequest,System.Threading.CancellationToken)~System.Threading.Tasks.Task{VinylStore.Cart.Domain.Responses.Cart.CartExtendedResponse}")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Cart.Domain.Handlers.Cart.UpdateCartItemQuantity.#ctor(VinylStore.Cart.Domain.Infrastructure.Repositories.ICartRepository,AutoMapper.IMapper,VinylStore.Cart.Domain.Infrastructure.CatalogEnricher.ICatalogEnricher)")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Cart.Domain.Handlers.Cart.UpdateCartItemQuantity.Handle(VinylStore.Cart.Domain.Requests.Cart.UpdateCartItemQuantityRequest,System.Threading.CancellationToken)~System.Threading.Tasks.Task{VinylStore.Cart.Domain.Responses.Cart.CartExtendedResponse}")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Cart.Domain.Infrastructure.CatalogEnricher.CatalogEnricher.#ctor(VinylStore.Catalog.API.Client.ICatalogClient)")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Cart.Domain.Infrastructure.CatalogEnricher.CatalogEnricher.EnrichCartItem(VinylStore.Cart.Domain.Responses.Cart.CartItemResponse,System.Threading.CancellationToken)~System.Threading.Tasks.Task{VinylStore.Cart.Domain.Responses.Cart.CartItemResponse}")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target = "~M:VinylStore.Cart.Domain.Infrastructure.Mapper.CartProfile.#ctor")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target = "~P:VinylStore.Cart.Domain.Requests.Cart.UpdateCartItemQuantityRequest.IsRemoveOperation")]