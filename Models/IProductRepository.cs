using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public interface IProductRepository
    {
        IEnumerable<ProductResultDto> GetAll();
        IEnumerable<ProductResultDto> GetByName(String searchString);
        ProductResultDto Get(Guid id);
        ProductResultDto Add(ProductDto product);
        ProductResultDto Update(Guid id, ProductDto product);
        ProductResultDto Remove(Guid id);
    }
}