using System.ComponentModel.DataAnnotations;

namespace FurnitureOrderingSystemAdmin.Models
{
    public class ComboItem
    {
        public int Id { get; set; }
        
        [Required]
        public int ComboId { get; set; }
        
        [Required]
        public int FurnitureId { get; set; }
        
        public int Quantity { get; set; } = 1;

        [Required]
        public decimal ItemDiscountAmount { get; set; }

        // Navigation properties
        public virtual Combo Combo { get; set; } = null!;
        public virtual Furniture Furniture { get; set; } = null!;
    }
}
