using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET api/product
        [HttpGet]
        public IEnumerable<ProductDTO> Get()
        {
            return this.ProductRepo.GetAll();
        }

        [HttpGet("{id}", Name = "Get Product By Id")]
        public ProductDTO GetById(Guid id)
        {
            return this.ProductRepo.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] ProductDTO product)
        {
            this.ProductRepo.Add(product);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProductDTO product)
        {
            if (product == null || product.Id == Guid.Empty)
            {
                return BadRequest();
            }

            this.ProductRepo.Update(product);
            return Accepted();
        }
    }
}