using System;
using System.Collections.Generic;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductController : Controller
    {
        public ProductController(IProductRepository productRepo)
        {
            this.ProductRepo = productRepo;
        }

        public IProductRepository ProductRepo { get; set; }

        // GET api/product?name={productName}
        /// <summary> Get all the products. </summary>
        /// <param name="name">Name of the product which you would like to filter with</param>
        [HttpGet]
        public IEnumerable<ProductResultDto> Get([FromQuery] String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return this.ProductRepo.GetAll();
            }
            else
            {
                return this.ProductRepo.GetByName(name);
            }
        }

        // GET api/product/{id}
        /// <summary> Get the product by its unique identifier</summary>
        /// <param name="id">Unique identifier of the product</param>
        [HttpGet("{id:Guid}", Name = "GetProduct")]
        [ProducesResponseTypeAttribute(typeof(ProductDto), 200)]
        public IActionResult GetById(Guid id)
        {
            ProductResultDto productResultDTO = this.ProductRepo.Get(id);

            if (productResultDTO == null)
            {
                return NotFound("Product is not found.");
            }

            return new OkObjectResult(productResultDTO);
        }

        // POST api/product
        /// <summary>Create a new product</summary>
        /// <param name="product">Details of the product to be created</param>
        [HttpPost]
        [ProducesResponseTypeAttribute(typeof(ProductDto), 200)]
        public IActionResult Post([FromBody] ProductDto product)
        {
            ProductResultDto productResultDTO = this.ProductRepo.Add(product);
            return new OkObjectResult(productResultDTO);
        }

        // PUT api/product/{id}
        /// <summary>Create a new product</summary>
        /// <param name="id">Unique identifier of the product</param>
        /// <param name="product">Details of the product to be updated</param>
        [HttpPut("{id:Guid}")]
        [ProducesResponseTypeAttribute(typeof(ProductDto), 200)]
        public IActionResult Update(Guid id, [FromBody] ProductDto product)
        {
            ProductResultDto productResultDTO = this.ProductRepo.Update(id, product);

            if (productResultDTO == null)
            {
                return NotFound("Product is not found for updating.");
            }

            return new OkObjectResult(productResultDTO);
        }
    }
}