using System.ComponentModel.DataAnnotations;

namespace PersonalProject.BLL.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        [Required]
        public string ProductName { get; set; }
        
        [Display(Name = "Description")]
        public string? ProductDescription { get; set; }

        [Display(Name = "Price")]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be a positive number.")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Quantity")]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be a positive number.")]
        public int ProductQuantity { get; set; }
    }
}
