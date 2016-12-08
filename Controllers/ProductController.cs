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
        public IEnumerable<ProductDTO> Get([FromQuery] String name)
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
            ProductDTO productDTOResult = this.ProductRepo.Get(id);

            if (productDTOResult == null)
            {
                return NotFound("Product is not found.");
            }

            return new OkObjectResult(productDTOResult);
        }

        // POST api/product
        [HttpPost]
        [ProducesResponseTypeAttribute(typeof(ProductDTO), 200)]
        public IActionResult Post([FromBody] ProductDTO product)
        {
            ProductDTO productDTOResult = this.ProductRepo.Add(product);
            return new OkObjectResult(productDTOResult);
        }

        // PUT api/product
        [HttpPut]
        public IActionResult Update([FromBody] ProductDTO product)
        {
            ProductDTO productDTOResult = this.ProductRepo.Update(product);

            if (productDTOResult == null)
            {
                return NotFound("Product is not found for updating.");
            }

            return new OkObjectResult(productDTOResult);
        }
    }
}