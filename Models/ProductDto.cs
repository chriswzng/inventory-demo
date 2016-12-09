using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    /// <summary>Product Object</summary>
    public class ProductDto
    {
        /// <summary>Name of product</summary>
        [Required]

        public string Name { get; set; }
        
        /// <summary>URL link to the product's image</summary>
        public string Image { get; set; }
        
        /// <summary>Cost price of the product</summary>
        [Required]
        public double CostPrice { get; set; }

        /// <summary>Selling price of the product</summary>
        [Required]
        public double SellingPrice { get; set; }
    }
}