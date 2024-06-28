using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Publishing.Models.Commands.CustomerCommands;
using Domain.Publishing.Models.Queries.CustomerQueries;
using Domain.Publishing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WS51_MyEvent.Publishing.Controllers
{
    /// <summary>
    /// Controller to manage customer operations.
    /// </summary>
    /// <remarks>
    /// Provides endpoints to get all customers, get a customer by its ID, create a new customer, log in, register and update customer information.
    /// </remarks>
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly ICustomerCommandService _customerCommandService;
        private readonly ICustomerQueryService _customerQueryService;
        
        public CustomerController(ICustomerQueryService customerQueryService, ICustomerCommandService customerCommandService,
            IMapper mapper)
        {
            _customerQueryService = customerQueryService;
            _customerCommandService = customerCommandService;
            _mapper = mapper;
        }
        
        
        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     GET /api/customers
        ///
        /// </remarks>
        /// <returns>A list of customers if found.</returns>
        /// <response code="200">Returns the list of customers.</response>
        /// <response code="404">If no customers are found.</response>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _customerQueryService.Handle(new GetAllCustomersQuery());
            if (result.Count == 0) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Gets a customer by their ID.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     GET /api/customers/{id}
        ///
        /// </remarks>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>The details of the customer if found.</returns>
        /// <response code="200">Returns the customer details.</response>
        /// <response code="404">If the customer is not found.</response>
        [HttpGet("{id}", Name = "GetAsync")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _customerQueryService.Handle(new GetCustomerByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }
        
        /// <summary>
        /// Logs in an existing customer.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/customers/login
        ///     {
        ///        "username": "johndoe",
        ///        "password": "password123"
        ///     }
        ///
        /// </remarks>
        /// <param name="command">The login command.</param>
        /// <returns>An access token if the login is successful.</returns>
        /// <response code="200">Returns the newly created access token.</response>
        /// <response code="400">If the command is null or invalid.</response>
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInCommand command)
        {
            var result =await _customerCommandService.Handle(command);
            return Ok(result);
        }
        
        /// <summary>
        /// Registers a new customer.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/customers/register
        ///     {
        ///        "username": "johndoe",
        ///        "password": "password123",
        ///        "email": "johndoe@example.com",
        ///        "userImage": "http://example.com/image.jpg",
        ///        "isArtisan": true,
        ///        "role": "user"
        ///     } 
        ///
        /// </remarks>
        /// <param name="command">The registration command.</param>
        /// <returns>An access token if the registration is successful.</returns>
        /// <response code="200">Returns the newly created access token.</response>
        /// <response code="400">If the command is null or invalid.</response>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]SignUpCommand command)
        {
            if (command.Role == null)
            {
                command.Role = "user";
            }
            var result = await _customerCommandService.Handle(command);
            return Ok(result);
        }
        
        
    }
}
