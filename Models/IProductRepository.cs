using System;
using System.Collections.Generic;

namespace Inventory.Models
{
    public interface IProductRepository
    {
        IEnumerable<ProductResult> GetAll();
        IEnumerable<ProductResult> GetByName(String searchString);
        ProductResult Get(Guid id);
        ProductResult Add(Product product);
        ProductResult Update(Guid id, Product product);
        ProductResult Remove(Guid id);
    }
}