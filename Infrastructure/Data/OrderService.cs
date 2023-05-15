using Core.entites;
using Core.entites.OrderAggragate;
using Core.Interfaces;
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

        public OrderService(IBasketRepo _basketRepo, IUnitOfWork _unitOfWork)
        {
            basketRepo = _basketRepo;
            unitOfWork = _unitOfWork;
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
                var OrderItem = new OrderItem(itemOrdered, produtItem.Price, item.quntity);

                OrderItems.Add(OrderItem);
            }

            var delivaryMethod = await unitOfWork.Repositary<DelivaryMethod>().GetById(delivaryMethodId);

            //calcute subtotal 
            var subtotal = OrderItems.Sum(item=> item.Price * item.Qunatity);

            var order = new Order(buyerEmail,shippingAddress,delivaryMethod,OrderItems,
                subtotal);

            await unitOfWork.Repositary<Order>().AddEntityAsync(order);
            var res= await unitOfWork.Complete();

            if (res < 0)
                return null;


            await basketRepo.DeleteBasketAsync(BasketId);

            return order;


        }

        public Task<Order> CreateOrderAsync(int id, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DelivaryMethod>> GetDelivaryMethodAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
