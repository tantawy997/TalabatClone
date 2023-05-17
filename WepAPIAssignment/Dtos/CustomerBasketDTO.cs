using Core.entites;

namespace WepAPIAssignment.Dtos
{
    public class CustomerBasketDTO
    {
        //public CustomerBasketDTO(string _id)
        //{
        //    id = _id;
        //}
        public string Id { get; set; }

        public int? DelivaryMethodId { get; set; }

        public decimal ShippingPrice { get; set; }

        public List<BasketItemsDTO> BasketItems { get; set; }

        public string PaymentIntentId { get; set; }

        public string ClientSecret { get; set; }

    }
}
