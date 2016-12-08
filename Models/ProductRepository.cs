using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Models
{
    public class ProductRepository : IProductRepository
    {
        private InventoryContext _inventoryContext;

        public ProductRepository(InventoryContext InventoryContext)
        {
            this._inventoryContext = InventoryContext;
        }

        public ProductDTO Get(Guid id)
        {
            var product = this._inventoryContext.Products.Where(p => p.Id == id).FirstOrDefault();
            ProductDTO productDTO = null;

            if (product != null)
            {
                productDTO = mapDTO(product);
            }

            return productDTO;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            List<Product> productList = this._inventoryContext.Products.ToList();
            List<ProductDTO> productDTOList = new List<ProductDTO>();

            foreach (var product in productList)
            {
                productDTOList.Add(mapDTO(product));
            }

            return productDTOList;
        }

        public IEnumerable<ProductDTO> GetByName(String searchString)
        {
            List<Product> productList = this._inventoryContext.Products.Where(p => p.Name.Contains(searchString)).ToList();


            List<ProductDTO> productDTOList = new List<ProductDTO>();

            foreach (var product in productList)
            {
                productDTOList.Add(mapDTO(product));
            }

            return productDTOList;
        }

        public ProductDTO Add(ProductDTO productDTO)
        {
            productDTO.Id = Guid.NewGuid();
            Product product = map(productDTO);

            this._inventoryContext.Add(product);
            this._inventoryContext.SaveChanges();

            return productDTO;
        }

        public ProductDTO Update(ProductDTO productDTO)
        {
            Product productFound = this._inventoryContext.Products.Where(p => p.Id == productDTO.Id).FirstOrDefault();

            if (productFound != null)
            {
                productFound.Name = productDTO.Name;
                productFound.Image = productDTO.Image;
                productFound.CostPrice = productDTO.CostPrice;
                productFound.SellingPrice = productDTO.SellingPrice;

                this._inventoryContext.SaveChanges();
            }
            else
            {
                productDTO = null;
            }

            return productDTO;
        }

        public ProductDTO Remove(Guid id)
        {
            throw new NotImplementedException();
        }



        // This can be replaced by AutoMapper
        private ProductDTO mapDTO(Product product)
        {
            ProductDTO productDTO = new ProductDTO();
            productDTO.Id = product.Id;
            productDTO.Name = product.Name;
            productDTO.Image = product.Image;
            productDTO.SellingPrice = product.SellingPrice;
            productDTO.CostPrice = product.CostPrice;

            return productDTO;
        }

        private Product map(ProductDTO productDTO)
        {
            Product product = new Product();
            product.Id = productDTO.Id;
            product.Name = productDTO.Name;
            product.Image = productDTO.Image;
            product.SellingPrice = productDTO.SellingPrice;
            product.CostPrice = productDTO.CostPrice;

            return product;
        }
    }
}
