using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.BusinessCommon;
using Checkout.Domain;
using Checkout.Domain.Models;
using Checkout.Domain.Requests;
using Checkout.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(IUserService userService, IOrdersRepository ordersRepository)
        {
            _userService = userService;
            _ordersRepository = ordersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IList<OrderModel>>> GetAllOrders() =>
            (await _ordersRepository.GetAllOrdersForUser(GetUserId())).ToArray();

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderModel>> GetOrderById(string orderId) =>
            await _ordersRepository.GetOrderById(GetUserId(), orderId);

        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateOrder() =>
            await _ordersRepository.CreateOrderForUser(GetUserId());

        [HttpPost("addToOrder/{orderId}")]
        public async Task AddItemToOrder(string orderId, [FromBody] CreateOrderItemRequest item) =>
            await _ordersRepository.AddItemToOrder(GetUserId(), orderId, item);

        [HttpPost("changeOrderItem/{orderId}")]
        public async Task ChangeOrderItemQuantity(string orderId, [FromBody] ChangeOrderItemRequest request) =>
            await _ordersRepository
                .ChangeOrderItemQuantity(GetUserId(), orderId, request.OrderItemId, request.Quantity);

        [HttpPost("removeOrderItem/{orderId}")]
        public async Task RemoveOrderItemQuantity(string orderId, [FromBody] RemoveOrderItemRequest request) =>
            await _ordersRepository
                .RemoveItemFromOrder(GetUserId(), orderId, request.OrderItemId);

        [HttpPost("purge/{orderId}")]
        public async Task PurgeOrder(string orderId) =>
            await _ordersRepository
                .PurgeOrder(GetUserId(), orderId);


        private ulong GetUserId() => _userService.GetCurrentUserId(User);
    }
}
