using Core.entites.OrderAggragate;

namespace WepAPIAssignment.Dtos
{
    public class OrderDetailsDTO
    {
        public int id { get; set; }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public ShippingAddressDTO shippingAddress { get; set; }

        public string delivaryMethod { get; set; }

        public decimal ShippingPrice { get; set; }

        public IReadOnlyList<OrderItemDTO> orderItems { get; set; }

        public decimal subTotal { get; set; }

        public string orderStatus { get; set; }

        public decimal Total { get; set; }

    }
}
