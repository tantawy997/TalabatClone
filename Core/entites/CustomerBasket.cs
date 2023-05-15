namespace Core.entites
{
    public class CustomerBasket
    {
        public CustomerBasket(string Id)
        {
            id = Id;
        }

        public string id { get; set; }

        public int? DelivaryMethod { get; set; }

        public decimal ShippingPrice { get; set; }

        public List<BasketItems> BasketItems { get;set; } = new List<BasketItems>();



    }
}
