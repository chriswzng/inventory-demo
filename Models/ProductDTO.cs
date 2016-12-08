using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public string Image { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        public double CostPrice { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double SellingPrice { get; set; }
    }
}