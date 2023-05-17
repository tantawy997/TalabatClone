using Core.entites;
using Core.entites.OrderAggragate;
using Core.Interfaces;
using Core.Specifiactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepo basketRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPaymentService paymentService;

        public OrderService(IBasketRepo _basketRepo, IUnitOfWork _unitOfWork, IPaymentService _paymentService)
        {
            basketRepo = _basketRepo;
            unitOfWork = _unitOfWork;
            paymentService = _paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int delivaryMethodId, 
            string BasketId, ShippingAddress shippingAddress)
        {
            var basket = await basketRepo.GetBasketAsync(BasketId);

            var OrderItems = new List<OrderItem>();

            foreach(var item in basket.BasketItems)
            {
                var produtItem = await unitOfWork.Repositary<Product>().GetById(item.Id);

                var itemOrdered = new ProductItemOrdered(produtItem.Id,produtItem.Name,
                    produtItem.PictureUrl);
                var OrderItem = new OrderItem(itemOrdered, produtItem.Price, item.Quntity);

                OrderItems.Add(OrderItem);
            }

            var delivaryMethod = await unitOfWork.Repositary<DelivaryMethod>().GetById(delivaryMethodId);

            //calcute subtotal 
            var subtotal = OrderItems.Sum(item=> item.Price * item.Qunatity);

            var spec = new OrderWithPaymentSpecefactions(basket.PaymentIntentId);
            var ExistingOrder = await unitOfWork.Repositary<Order>().GetEntitytWithSpecifiaction(spec);
            if(ExistingOrder != null)
            {
                unitOfWork.Repositary<Order>().Delete(ExistingOrder);
                await paymentService.CreateOrUpdatePaymentIntent(BasketId);
            }

            var order = new Order(buyerEmail,shippingAddress,delivaryMethod,OrderItems,
                subtotal);

            await unitOfWork.Repositary<Order>().AddEntityAsync(order);
            var res= await unitOfWork.Complete();

            if (res < 0)
                return null;


            await basketRepo.DeleteBasketAsync(BasketId);

            return order;


        }

        public async Task<Order> CreateOrderByIdAsync(int id, string buyerEmail)
        {
            OrderWithItemsSpecefactions orderSpec = new OrderWithItemsSpecefactions(id, buyerEmail);

            return await unitOfWork.Repositary<Order>().GetEntitytWithSpecifiaction(orderSpec);

        }

        public async Task<IReadOnlyList<DelivaryMethod>> GetDelivaryMethodAsync()
        {
            return await unitOfWork.Repositary<DelivaryMethod>().GetAllAsync();
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            OrderWithItemsSpecefactions orderSpec = new OrderWithItemsSpecefactions(buyerEmail);
            return await unitOfWork.Repositary<Order>().ListUnderSpecifications(orderSpec);

        }
    }
}
