using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Publishing.Models.Commands.OrderCommands;
using Domain.Publishing.Models.Queries.OrderQueries;
using Domain.Publishing.Services.OrderServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WS51_MyEvent.Publishing.Controllers
{
    /// <summary>
    /// Controller to manage order operations.
    /// </summary>
    /// <remarks>
    /// Provides endpoints to get all orders, get an order by its ID, and create a new order.
    /// </remarks>
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderCommandService _orderCommandService;
        private readonly IOrderQueryService _orderQueryService;
        
        public OrderController(IOrderQueryService orderQueryService, IOrderCommandService commandService,
            IMapper mapper)
        {
            _orderCommandService = commandService;
            _orderQueryService = orderQueryService;
            _mapper = mapper;
        }
        // GET: api/orders
        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     GET /api/orders
        ///
        /// </remarks>
        /// <returns>A list of orders if found.</returns>
        /// <response code="200">Returns the list of orders.</response>
        /// <response code="404">If no orders are found.</response>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _orderQueryService .Handle(new GetAllOrdersQuery());
            if (result.Count == 0) return NotFound();
            return Ok(result);
        }

        // GET: api/orders/5
        /// <summary>
        /// Gets an order by its ID.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     GET /api/orders/{id}
        ///
        /// </remarks>
        /// <param name="id">The ID of the order.</param>
        /// <returns>The details of the order if found.</returns>
        /// <response code="200">Returns the order details.</response>
        /// <response code="404">If the order is not found.</response>
        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _orderQueryService.Handle(new GetOrderByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/orders/create
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/orders/create
        ///     {
        ///        "productId": "123",
        ///        "product": "Product Name",
        ///        "parameters": [
        ///          {
        ///            "paramName": "Color",
        ///            "paramValue": "Red"
        ///          }
        ///        ],
        ///        "price": 100.0
        ///     }
        ///
        /// </remarks>
        /// <param name="command">The command to create an order.</param>
        /// <returns>The ID of the created order if the creation is successful.</returns>
        /// <response code="201">Returns the ID of the newly created order.</response>
        /// <response code="400">If the command is null or invalid.</response>
        [HttpPost("create")]
        public async Task<IActionResult> PostAsync([FromBody] CreateOrderCommand command)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _orderCommandService.Handle(command);
            if (result > 0)
                return StatusCode(StatusCodes.Status201Created, result);
            return BadRequest();
        }
        
    }
}
