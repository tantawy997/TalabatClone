namespace Core.entites
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public int? DelivaryMethod { get; set; }

        public decimal ShippingPrice { get; set; }

        public List<BasketItems> BasketItems { get;set; } = new List<BasketItems>();



    }
}
