using AutoMapper;
using Core.entites.OrderAggragate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPIAssignment.Dtos;
using WepAPIAssignment.extentsions;
using WepAPIAssignment.ResponseModule;

namespace WepAPIAssignment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService _orderService,IMapper _mapper)
        {
            orderService = _orderService;
            mapper = _mapper;
        }

        [HttpPost("CreateOrder")] 

        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var email = HttpContext.User.RetriveEmailFromPrincipal();

            var address = mapper.Map<ShippingAddress>(orderDTO.Address);

            var order = await orderService.CreateOrderAsync(email,orderDTO.DeliveryMethodId,
                orderDTO.BasketId,address);

            if (order is null)
                return BadRequest(new ApiResponse(404,"Problem When Creating Order"));

            return Ok(order);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrderByUserId(int id)
        {
            var email = HttpContext.User.RetriveEmailFromPrincipal();

            var order = await orderService.CreateOrderByIdAsync(id , email);

            if (order is null)
                return NotFound(new ApiResponse(404, "Order does not exist"));

            return Ok(mapper.Map<OrderDetailsDTO>(order)); 
        }

        [HttpGet("GetAllOrdersForUser")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetriveEmailFromPrincipal();

            var userOrders = await orderService.GetOrdersForUserAsync(email);

            return Ok(mapper.Map<IReadOnlyList<OrderDetailsDTO>>(userOrders));
        }

        [HttpGet("DelivaryMethod")]

        public async Task<ActionResult<IReadOnlyList<DelivaryMethod>>> GetDeliveryMethods()
        {
            return Ok(await orderService.GetDelivaryMethodAsync());
        }
         
    }
}
