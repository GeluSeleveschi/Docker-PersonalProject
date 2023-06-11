namespace PersonalProject.BLL.Models;
public class ShoppingCartModel
{
    public List<ProductToSellModel> ProductsToSell { get; set; } = new List<ProductToSellModel>();
    public List<ProductToSellModel> ShoppingCartProducts { get; set; } = new List<ProductToSellModel>();
    public decimal TotalCost { get; set; }
}
