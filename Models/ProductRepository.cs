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

        public ProductResultDto Get(Guid id)
        {
            var product = this._inventoryContext.Products.Where(p => p.Id == id).FirstOrDefault();
            ProductResultDto productResultDTO = null;

            if (product != null)
            {
                productResultDTO = mapDTO(product);
            }

            return productResultDTO;
        }

        public IEnumerable<ProductResultDto> GetAll()
        {
            List<Product> productList = this._inventoryContext.Products.ToList();
            List<ProductResultDto> productResultDTOList = new List<ProductResultDto>();

            foreach (var product in productList)
            {
                productResultDTOList.Add(mapDTO(product));
            }

            return productResultDTOList;
        }

        public IEnumerable<ProductResultDto> GetByName(String searchString)
        {
            List<Product> productList = this._inventoryContext.Products.Where(p => p.Name.Contains(searchString)).ToList();
            List<ProductResultDto> productResultDTOList = new List<ProductResultDto>();

            foreach (var product in productList)
            {
                productResultDTOList.Add(mapDTO(product));
            }

            return productResultDTOList;
        }

        public ProductResultDto Add(ProductDto productDTO)
        {
            Product product = map(productDTO);
            product.Id = Guid.NewGuid();

            this._inventoryContext.Add(product);
            this._inventoryContext.SaveChanges();

            return mapDTO(product);
        }

        public ProductResultDto Update(Guid id, ProductDto productDTO)
        {
            Product productFound = this._inventoryContext.Products.Where(p => p.Id == id).FirstOrDefault();

            if (productFound != null)
            {
                productFound.Name = productDTO.Name;
                productFound.Image = productDTO.Image;
                productFound.CostPrice = productDTO.CostPrice;
                productFound.SellingPrice = productDTO.SellingPrice;

                this._inventoryContext.SaveChanges();
                return mapDTO(productFound);
            }

            return null;
        }

        public ProductResultDto Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        // This can be replaced by AutoMapper
        private ProductResultDto mapDTO(Product product)
        {
            ProductResultDto productResultDTO = new ProductResultDto();
            productResultDTO.Id = product.Id;
            productResultDTO.Name = product.Name;
            productResultDTO.Image = product.Image;
            productResultDTO.SellingPrice = product.SellingPrice;
            productResultDTO.CostPrice = product.CostPrice;

            return productResultDTO;
        }

        private Product map(ProductDto productDTO)
        {
            Product product = new Product();
            product.Name = productDTO.Name;
            product.Image = productDTO.Image;
            product.SellingPrice = productDTO.SellingPrice;
            product.CostPrice = productDTO.CostPrice;

            return product;
        }
    }
}
