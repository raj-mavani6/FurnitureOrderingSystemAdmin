using System.ComponentModel.DataAnnotations;

namespace FurnitureOrderingSystemAdmin.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public decimal UnitPrice { get; set; }
        
        [Required]
        public decimal TotalPrice { get; set; }
        
        // Foreign Keys
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        
        public int FurnitureId { get; set; }
        public virtual Furniture Furniture { get; set; } = null!;
    }
}
