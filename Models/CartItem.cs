using System.ComponentModel.DataAnnotations;

namespace FurnitureOrderingSystemAdmin.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Foreign Keys
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;
        
        public int FurnitureId { get; set; }
        public virtual Furniture Furniture { get; set; } = null!;
    }
}
