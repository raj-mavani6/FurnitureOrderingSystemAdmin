using System.ComponentModel.DataAnnotations;

namespace FurnitureOrderingSystemAdmin.Models
{
    public class Combo
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [StringLength(255)]
        public string? ImageUrl { get; set; }
        
        [Required]
        public decimal OriginalPrice { get; set; }
        
        [Required]
        public decimal DiscountedPrice { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        public virtual ICollection<ComboItem> ComboItems { get; set; } = [];
    }
}
