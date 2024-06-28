using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Publishing.Models.Commands.ProductCommands;
using Domain.Publishing.Models.Queries.ProductQueries;
using Domain.Publishing.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WS51_MyEvent.Publishing.Controllers
{
    /// <summary>
    /// Controller to manage product operations.
    /// </summary>
    /// <remarks>
    /// Provides endpoints to get all products, get a product by its ID, and create a new product.
    /// </remarks>
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;
        
        public ProductController(IProductQueryService productQueryService, IProductCommandService commandService,
            IMapper mapper)
        {
            _productCommandService = commandService;
            _productQueryService = productQueryService;
            _mapper = mapper;
        }
        // GET: api/Product
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     GET /api/products
        ///
        /// </remarks>
        /// <returns>A list of products if found.</returns>
        /// <response code="200">Returns the list of products.</response>
        /// <response code="404">If no products are found.</response>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _productQueryService .Handle(new GetAllProductsQuery());
            if (result.Count == 0) return NotFound();
            return Ok(result);
        }

        // GET: api/Product/5
        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     GET /api/products/{id}
        ///
        /// </remarks>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The details of the product if found.</returns>
        /// <response code="200">Returns the product details.</response>
        /// <response code="404">If the product is not found.</response>
        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _productQueryService.Handle(new GetProductByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/Product
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/products/create
        ///     {
        ///        "name": "Product",
        ///        "description": "Product description",
        ///        "price": 100.0,
        ///        "details": "Product details",
        ///        "artisanDetails": "Artisan details",
        ///        "customizationParameters": {
        ///          "parameters": [
        ///            {
        ///              "name": "Color",
        ///              "values": [
        ///                {
        ///                  "value": "Red"
        ///                }
        ///              ]
        ///            }
        ///          ],
        ///          "engraving": "Custom engraving"
        ///        },
        ///        "size": 10.0,
        ///        "inputText": "Input text",
        ///        "engraving": "Product engraving",
        ///        "category": "Product category",
        ///        "image": "http://example.com/image.jpg",
        ///        "detailImages": [
        ///          {
        ///            "imageUrl": "http://example.com/detail_image.jpg"
        ///          }
        ///        ],
        ///        "author": "Product author",
        ///        "features": [
        ///          {
        ///            "name": "Material",
        ///            "value": "Wood"
        ///          }
        ///        ]
        ///     }
        ///
        /// </remarks>
        /// <param name="command">The command to create a product.</param>
        /// <returns>The ID of the created product if the creation is successful.</returns>
        /// <response code="201">Returns the ID of the newly created product.</response>
        /// <response code="400">If the command is null or invalid.</response>
        [HttpPost("create")]
        public async Task<IActionResult> PostAsync([FromBody] CreateProductCommand command)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _productCommandService.Handle(command);
            if (result > 0)
                return StatusCode(StatusCodes.Status201Created, result);
            return BadRequest();
        }
        
        // DELETE: api/Product/5
        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <remarks>
        /// Request example:
        ///
        ///     DELETE /api/products/{id}
        ///
        /// </remarks>
        /// <param name="id">The ID of the product.</param>
        /// <returns>A status code indicating the result of the operation.</returns>
        /// <response code="204">If the product is successfully deleted.</response>
        /// <response code="404">If the product is not found.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productCommandService.DeleteAsync(id);
            if (result)
                return NoContent();
            return NotFound();
        }
        
    }
}
