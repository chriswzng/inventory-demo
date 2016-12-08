using System;
using System.Collections.Generic;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/product")]
    public class ProductController : Controller
    {
        public ProductController(IProductRepository productRepo)
        {
            this.ProductRepo = productRepo;
        }

        public IProductRepository ProductRepo { get; set; }

        // GET api/product?name={productName}
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
        [HttpPost]
        [ProducesResponseTypeAttribute(typeof(ProductDto), 200)]
        public IActionResult Post([FromBody] ProductDto product)
        {
            ProductResultDto productResultDTO = this.ProductRepo.Add(product);
            return new OkObjectResult(productResultDTO);
        }

        // PUT api/product/{id}
        [HttpPut("{id:Guid}")]
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