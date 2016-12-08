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
        public IEnumerable<ProductResultDTO> Get([FromQuery] String name)
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
        [ProducesResponseTypeAttribute(typeof(ProductDTO), 200)]
        public IActionResult GetById(Guid id)
        {
            ProductResultDTO productResultDTO = this.ProductRepo.Get(id);

            if (productResultDTO == null)
            {
                return NotFound("Product is not found.");
            }

            return new OkObjectResult(productResultDTO);
        }

        // POST api/product
        [HttpPost]
        [ProducesResponseTypeAttribute(typeof(ProductDTO), 200)]
        public IActionResult Post([FromBody] ProductDTO product)
        {
            ProductResultDTO productResultDTO = this.ProductRepo.Add(product);
            return new OkObjectResult(productResultDTO);
        }

        // PUT api/product/{id}
        [HttpPut("{id:Guid}")]
        public IActionResult Update(Guid id, [FromBody] ProductDTO product)
        {
            ProductResultDTO productResultDTO = this.ProductRepo.Update(id, product);

            if (productResultDTO == null)
            {
                return NotFound("Product is not found for updating.");
            }

            return new OkObjectResult(productResultDTO);
        }
    }
}