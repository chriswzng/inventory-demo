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

        public ProductResult Get(Guid id)
        {
            var product = this._inventoryContext.Products.Where(p => p.Id == id).FirstOrDefault();
            ProductResult productResultDTO = null;

            if (product != null)
            {
                productResultDTO = mapDTO(product);
            }

            return productResultDTO;
        }

        public IEnumerable<ProductResult> GetAll()
        {
            List<ProductEnt> productList = this._inventoryContext.Products.ToList();
            List<ProductResult> productResultDTOList = new List<ProductResult>();

            foreach (var product in productList)
            {
                productResultDTOList.Add(mapDTO(product));
            }

            return productResultDTOList;
        }

        public IEnumerable<ProductResult> GetByName(String searchString)
        {
            List<ProductEnt> productList = this._inventoryContext.Products.Where(p => p.Name.Contains(searchString)).ToList();
            List<ProductResult> productResultDTOList = new List<ProductResult>();

            foreach (var product in productList)
            {
                productResultDTOList.Add(mapDTO(product));
            }

            return productResultDTOList;
        }

        public ProductResult Add(Product productDTO)
        {
            ProductEnt product = map(productDTO);
            product.Id = Guid.NewGuid();

            this._inventoryContext.Add(product);
            this._inventoryContext.SaveChanges();

            return mapDTO(product);
        }

        public ProductResult Update(Guid id, Product productDTO)
        {
            ProductEnt productFound = this._inventoryContext.Products.Where(p => p.Id == id).FirstOrDefault();

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

        public ProductResult Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        // This can be replaced by AutoMapper
        private ProductResult mapDTO(ProductEnt product)
        {
            ProductResult productResultDTO = new ProductResult();
            productResultDTO.Id = product.Id;
            productResultDTO.Name = product.Name;
            productResultDTO.Image = product.Image;
            productResultDTO.SellingPrice = product.SellingPrice;
            productResultDTO.CostPrice = product.CostPrice;

            return productResultDTO;
        }

        private ProductEnt map(Product productDTO)
        {
            ProductEnt product = new ProductEnt();
            product.Name = productDTO.Name;
            product.Image = productDTO.Image;
            product.SellingPrice = productDTO.SellingPrice;
            product.CostPrice = productDTO.CostPrice;

            return product;
        }
    }
}
