using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace TrainingRazor.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(500)]
        public string Name { get; set; }
    }

    public class CustPurchased
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public ApplicationUser Creator { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public Product Product { get; set; }
    }

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}