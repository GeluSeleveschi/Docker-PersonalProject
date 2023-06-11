using PersonalProject.BLL.Entities;
using PersonalProject.BLL.Models;

namespace PersonalProject.BLL.Services.Interfaces
{
    public interface IProductService
    {
        #region Product CRUD

        bool AddProduct(ProductModel model);
        List<Product> GetAllProducts();
        ProductModel GetProductById(int? id);
        bool UpdateProduct(ProductModel model);
        bool DeleteProduct(int productId);

        #endregion

        #region Shopping Cart

        ShoppingCartModel GetAllProductsToSell();
        ShoppingCartModel AddProductsToSellToShoppingCart(ProductToSellModel productToSell);
        ShoppingCartModel ShoppingCartProducts();
        bool DeleteProductFromShoppingCart(int productId);

        #endregion

        #region Order

        bool SaveOrder();
        List<OrderViewModel> GetAllOrders();

        #endregion
    }
}
