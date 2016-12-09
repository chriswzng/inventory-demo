using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
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