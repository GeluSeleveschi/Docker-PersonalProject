using PersonalProject.BLL.Entities;
using PersonalProject.BLL.Models;

namespace PersonalProject.BLL.Services.Interfaces
{
    public interface IProductService
    {
        bool AddProduct(ProductViewModel model);
        List<Product> GetAllProducts();
        ProductViewModel GetProductById(int? id);
        bool UpdateProduct(ProductViewModel model);
        bool DeleteProduct(int productId);
    }
}
