using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public interface IProductRepository
    {
        IEnumerable<ProductResultDTO> GetAll();
        IEnumerable<ProductResultDTO> GetByName(String searchString);
        ProductResultDTO Get(Guid id);
        ProductResultDTO Add(ProductDTO product);
        ProductResultDTO Update(Guid id, ProductDTO product);
        ProductResultDTO Remove(Guid id);
    }
}