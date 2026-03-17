using System.ComponentModel.DataAnnotations;

namespace FurnitureOrderingSystemAdmin.Models
{
    public class Furniture
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Furniture name is required")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        
        [StringLength(255)]
        public string? ImageUrl { get; set; }
        
        [StringLength(50)]
        public string? Color { get; set; }
        
        [StringLength(50)]
        public string? Material { get; set; }
        
        [StringLength(100)]
        public string? Dimensions { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; set; } = 0;
        
        public bool IsAvailable { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Foreign Key
        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        // Navigation properties
        public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
        public virtual ICollection<CartItem> CartItems { get; set; } = [];
    }
}
