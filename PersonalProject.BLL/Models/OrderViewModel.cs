using System.ComponentModel.DataAnnotations;

namespace PersonalProject.BLL.Models
{
    public class OrderViewModel
    {
        [Display(Name ="Date Created")]
        public string? DateCreated { get; set; }
        
        [Display(Name = "Total Cost")]
        public decimal? TotalCost { get; set; }
    }
}
