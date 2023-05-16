using Core.entites.OrderAggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifiactions
{
    public class OrderWithItemsSpecefactions : BaseSpecifiactions<Order>
    {
        public OrderWithItemsSpecefactions(string email) : base(order=>order.BuyerEmail == email)
        {
            AddIncludes(order => order.delivaryMethod);
            AddIncludes(order => order.orderItems);
            AddOrderByDesc(Ord=>Ord.OrderDate);

        }
        public OrderWithItemsSpecefactions(int id,string email) : 
            base(order => order.id == id && order.BuyerEmail == email)
        {
            AddIncludes(order => order.delivaryMethod);
            AddIncludes(order => order.orderItems);
            AddOrderByDesc(Ord => Ord.OrderDate);

        }
    }
}
