using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureOrderingSystemAdmin.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; } = string.Empty;
        
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        [Required]
        public decimal TotalAmount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountAmount { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Shipped, Delivered, Cancelled
        
        [StringLength(500)]
        public string? DeliveryAddress { get; set; }
        
        [StringLength(100)]
        public string? DeliveryCity { get; set; }
        
        [StringLength(10)]
        public string? DeliveryPostalCode { get; set; }
        
        [StringLength(15)]
        public string? ContactPhone { get; set; }
        
        public DateTime? DeliveryDate { get; set; }
        
        [StringLength(1000)]
        public string? Notes { get; set; }
        
        // Foreign Key
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        // Navigation properties
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
