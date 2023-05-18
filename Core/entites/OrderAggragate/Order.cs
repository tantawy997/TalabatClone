using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.entites.OrderAggragate
{
    public class Order :BaseEntity
    {   
        public Order()
        {

        }
        public Order(string _buyerEmail, ShippingAddress _shippingAddress, 
            DelivaryMethod _delivaryMethod, IReadOnlyList<OrderItem> _orderItems, decimal _subTotal,
            string _PaymentIntentId)
        {
            BuyerEmail = _buyerEmail;
            shippingAddress = _shippingAddress;
            delivaryMethod = _delivaryMethod;
            orderItems = _orderItems;
            subTotal = _subTotal;
            PaymentIntentId = _PaymentIntentId;
        }

        public int id { get; set; }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public ShippingAddress shippingAddress { get; set; } 

        public DelivaryMethod delivaryMethod { get; set; }

        public IReadOnlyList<OrderItem> orderItems { get; set; }

        public decimal subTotal { get; set; }

        public OrderStatus orderStatus { get; set; } = OrderStatus.Pinding;

        public string? PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return subTotal + delivaryMethod.Price;
        }


    }
}
