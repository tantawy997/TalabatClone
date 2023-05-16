using AutoMapper;
using Core.entites;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPIAssignment.Dtos;
using WepAPIAssignment.ResponseModule;

namespace WepAPIAssignment.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketRepo basketRepo;
        private readonly IMapper mapper;

        public BasketController(IBasketRepo _basketRepo,IMapper _mpper)
        {
            basketRepo = _basketRepo;
            mapper = _mpper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDTO>> GetBasket(string id)
        {
            var basket = await basketRepo.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO customerBasket)
        {

            var basket = mapper.Map<CustomerBasket>(customerBasket);

            var updatedBasket = await basketRepo.UpdateBasketAsync(basket);

            return Ok(updatedBasket);
        }

        [HttpDelete]

        public async Task DeleteBasket(string id)
         {
            await basketRepo.DeleteBasketAsync(id);
        }
    }
}
