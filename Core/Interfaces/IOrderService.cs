using Core.entites.OrderAggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int delivaryMethod, string BusketId,
            ShippingAddress shippingAddress);

        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        
        Task<Order> CreateOrderAsync(int id, string buyerEmail);

        Task<IReadOnlyList<DelivaryMethod>> GetDelivaryMethodAsync();
    }
}
