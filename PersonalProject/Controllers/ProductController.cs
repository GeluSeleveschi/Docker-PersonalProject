using Microsoft.AspNetCore.Mvc;
using PersonalProject.BLL.Models;
using PersonalProject.BLL.Services.Interfaces;

namespace PersonalProject.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddEditProduct(int? id)
        {
            var productViewModel = _productService.GetProductById(id);

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult AddEditProduct(ProductModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = false;
            if (model != null && model.Id != 0)
                result = _productService.UpdateProduct(model);
            else
                result = _productService.AddProduct(model);

            return result ? RedirectToAction("GetAllProducts") : UnprocessableEntity(result);
        }

        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("GetAllProducts");
        }

        public IActionResult DisplayProductsToSell()
        {
            var model = _productService.GetAllProductsToSell();

            return View("ProductsToSell", model);
        }

        [HttpPost]
        public ActionResult AddProductsToShoppingCart(ProductToSellModel product)
        {
            var model = _productService.AddProductsToSellToShoppingCart(product);
            return Json(new { success = model.ShoppingCartProducts.Any() });
        }

        [HttpPost]
        public JsonResult DeleteProductFromShoppingCart(int productId)
        {
            var result = _productService.DeleteProductFromShoppingCart(productId);
            return Json(new { success = result });
        }

        [HttpPost]
        public JsonResult SaveOrder()
        {
            var result = _productService.SaveOrder();
            return Json(new { success = result });
        }

        [HttpGet]
        public ActionResult GetAllOrders()
        {
            var orders = _productService.GetAllOrders();
            return View("Orders", orders);
        }
    }
}
