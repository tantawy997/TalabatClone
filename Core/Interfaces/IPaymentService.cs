using Core.entites;
using Core.entites.OrderAggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId);

        Task<Order> UpdatePaymentFailed(string PaymentIntentId);

        Task<Order> UpdatePaymentSucceded(string PaymentIntentId);


    }
}
