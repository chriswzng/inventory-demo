namespace Inventory.Models
{
    public class ProductDto
    {      
        public string Name { get; set; }
        
        public string Image { get; set; }
        
        public double CostPrice { get; set; }

        public double SellingPrice { get; set; }
    }
}