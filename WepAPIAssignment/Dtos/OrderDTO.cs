namespace WepAPIAssignment.Dtos
{
    public class OrderDTO
    {
        public string BasketId { get; set; }

        public int DeliveryMethodId { get; set; }

        public ShippingAddressDTO Address { get; set; }




    }
}
