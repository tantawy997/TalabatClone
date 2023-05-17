namespace Core.entites
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public int? DelivaryMethodId { get; set; }

        public decimal ShippingPrice { get; set; }

        public List<BasketItems> BasketItems { get;set; } = new List<BasketItems>();

        public string PaymentIntentId { get; set; }

        public string ClientSecret { get; set; }

    }
}
