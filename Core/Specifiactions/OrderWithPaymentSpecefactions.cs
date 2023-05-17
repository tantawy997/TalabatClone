using Core.entites.OrderAggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifiactions
{
    public class OrderWithPaymentSpecefactions : BaseSpecifiactions<Order>
    {
        public OrderWithPaymentSpecefactions(string PaymentIntentId) 
            : base(option=>option.PaymentIntentId == PaymentIntentId)
        {
        }
    }
}
