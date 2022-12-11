using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingRazor.Models
{
    public class CustProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class InputCustPurchasingModel
    {
        public int Id { get; set; }

        public ApplicationUser Creator { get; set; }
        
        [Required(ErrorMessage = "Please select Product")]
        [Display(Name = "Product")]
        public int? ProductId { get; set; }

        [Required(ErrorMessage = "Please insert Quantity")]
        [Range(1, 999999999999, ErrorMessage = "Insert at least 1 quantity")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }

    public class InputProductModel
    {
        public int Id { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Please insert product name")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "Please insert product price")]
        [Display(Name = "Product Price (RM)")]
        public decimal Price { get; set; }
    }
}