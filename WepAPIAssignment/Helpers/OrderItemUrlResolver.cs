using AutoMapper;
using Core.entites.OrderAggragate;
using WepAPIAssignment.Dtos;

namespace WepAPIAssignment.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration configuration;

        public OrderItemUrlResolver(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
            {
                return configuration["ApiUrl"] + source.ItemOrdered.PictureUrl;
            }
            else
            {
                return configuration["ApiUrl"];
            }
        }
    }
}
