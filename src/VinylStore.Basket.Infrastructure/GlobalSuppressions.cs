// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Basket.Infrastructure.BasketRepository.#ctor(StackExchange.Redis.ConnectionMultiplexer)")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Basket.Infrastructure.BasketRepository.AddOrUpdateAsync(VinylStore.Basket.Domain.Entities.Basket)~System.Threading.Tasks.Task{VinylStore.Basket.Domain.Entities.Basket}")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Basket.Infrastructure.BasketRepository.DeleteBasketAsync(System.String)~System.Threading.Tasks.Task{System.Boolean}")]
[assembly:
    SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this",
        Justification = "<Pending>", Scope = "member",
        Target =
            "~M:VinylStore.Basket.Infrastructure.BasketRepository.GetAsync(System.Guid)~System.Threading.Tasks.Task{VinylStore.Basket.Domain.Entities.Basket}")]