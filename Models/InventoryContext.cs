using Microsoft.EntityFrameworkCore;

namespace Inventory.Models
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {}

        public DbSet<Product> Products { get; set; }
    }
}