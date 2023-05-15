namespace Core.entites.OrderAggragate
{
    public class OrderItem :BaseEntity
    {
        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int qunatity)
        {
            //id = id;
            ItemOrdered = itemOrdered;
            Price = price;
            Qunatity = qunatity;
        }

        OrderItem() { }

        public int id { get; set; }


        public ProductItemOrdered ItemOrdered { get; set; }

        public decimal Price { get; set; }

        public int Qunatity { get; set; }

    }
}