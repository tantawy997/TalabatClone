using Core.entites;
using Core.entites.OrderAggragate;
using Core.Interfaces;
using Core.Specifiactions;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepo basketRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public PaymentService(IBasketRepo _basketRepo,
            IUnitOfWork _unitOfWork, IConfiguration _configuration)
        {
            basketRepo = _basketRepo;
            unitOfWork = _unitOfWork;
            configuration = _configuration;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:Secretkey"];
            var basket = await basketRepo.GetBasketAsync(BasketId);

            if (basket == null)
                return null;
            decimal ShippingPrice = 0m;

            if (basket.DelivaryMethodId.HasValue)
            {
                var delivaryMethod = await unitOfWork.Repositary<DelivaryMethod>().GetById(basket.DelivaryMethodId.Value);
                ShippingPrice = delivaryMethod.Price;
            }

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                PaymentIntentCreateOptions options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.BasketItems.Sum(a => a.Quntity * (a.Price * 100)) + (long)ShippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card"}

                };
                intent = await service.CreateAsync(options);
                basket.ClientSecret = intent.ClientSecret;
                basket.PaymentIntentId = intent.Id;
            }
            else
            {
                PaymentIntentUpdateOptions options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.BasketItems.Sum(a => a.Quntity * (a.Price * 100)) + (long)ShippingPrice * 100
                    //Currency = "usd",
                    //PaymentMethodTypes = new List<string>() { "card" }

                };
                intent = await service.UpdateAsync(basket.PaymentIntentId,options);
                basket.ClientSecret = intent.ClientSecret;
                basket.PaymentIntentId = intent.Id;
            }
            await basketRepo.UpdateBasketAsync(basket);
            return basket;
        }
                    
        public async Task<Order> UpdatePaymentFailed(string PaymentIntentId)
        {
            var spec = new OrderWithPaymentSpecefactions(PaymentIntentId);

            var order =await unitOfWork.Repositary<Order>().GetEntitytWithSpecifiaction(spec);
            if (order is null)
                return null;

            order.orderStatus = OrderStatus.PaymentFailed;

            unitOfWork.Repositary<Order>().Update(order);

            await unitOfWork.Complete();

            return order;
        }

        public Task<Order> UpdatePaymentSucceded(string PaymentIntentId)
        {
            throw new NotImplementedException();
        }
    }
}
