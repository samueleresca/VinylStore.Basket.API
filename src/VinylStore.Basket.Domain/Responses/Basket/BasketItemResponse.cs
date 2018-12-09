namespace VinylStore.Basket.Domain.Responses.Basket
{
    public class BasketItemResponse
    {
        public string BasketItemId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string LabelName { get; set; }

        public string Price { get; set; }

        public string PictureUri { get; set; }

        public string GenreDescription { get; set; }

        public string ArtistName { get; set; }

        public int Quantity { get; set; }
    }
}