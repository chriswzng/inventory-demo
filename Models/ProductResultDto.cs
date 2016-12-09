using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class ProductResultDto : ProductDto
    {
        /// <summary>Unqiue identifier of the product</summary>
        [Required]
        public Guid Id { get; set; }        
    }
}