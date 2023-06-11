using System.ComponentModel.DataAnnotations;

namespace PersonalProject.BLL.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        
        [Display(Name = "Description")]
        public string ProductDescription { get; set; }

        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }
        
        [Display(Name = "Quantity")]
        public int ProductQuantity { get; set; }
    }
}
