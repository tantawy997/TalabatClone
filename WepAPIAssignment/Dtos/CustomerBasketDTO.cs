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

        public int? DelivaryMethod { get; set; }

        public decimal ShippingPrice { get; set; }

        public List<BasketItemsDTO> BasketItems { get; set; }

    }
}
