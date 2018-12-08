using System;

namespace VinylStore.Basket.Domain.Responses.Basket
{
    public class BasketItemResponse
    {
        public Guid BasketItemId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string LabelName { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        public string GenreDescription { get; set; }

        public string ArtistName { get; set; }
    }
}