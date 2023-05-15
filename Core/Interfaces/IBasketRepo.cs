using Core.entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBasketRepo
    {
        Task<CustomerBasket> GetBasketAsync(string id);

        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);

        Task DeleteBasketAsync(string id);


    }
}
