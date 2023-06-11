using PersonalProject.BLL.Entities;
using PersonalProject.BLL.Models;

namespace PersonalProject.BLL.Services.Interfaces
{
    public interface IProductService
    {
        bool AddProduct(ProductModel model);
        List<Product> GetAllProducts();
        ProductModel GetProductById(int? id);
        bool UpdateProduct(ProductModel model);
        bool DeleteProduct(int productId);

        ShoppingCartModel GetAllProductsToSell();
        ShoppingCartModel AddProductsToSellToShoppingCart(ProductToSellModel productToSell);
        ShoppingCartModel ShoppingCartProducts();
    }
}
