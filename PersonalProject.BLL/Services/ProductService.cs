using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PersonalProject.BLL.Entities;
using PersonalProject.BLL.Models;
using PersonalProject.BLL.Services.Interfaces;

namespace PersonalProject.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public ProductService(AppDbContext dbContext, ILogger<ProductService> logger, IMapper mapper, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public bool AddProduct(ProductModel model)
        {
            try
            {
                var product = _mapper.Map(model, new Product());
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();

                return product.Id != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.InnerException?.Message ?? ex?.Message);
                return false;
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                return _dbContext.Products.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.InnerException?.Message ?? ex?.Message);
                return new List<Product>();
            }
        }

        public ProductModel GetProductById(int? id)
        {
            try
            {
                if (id.HasValue && id.Value != 0)
                {
                    var product = _dbContext.Products.AsNoTracking().FirstOrDefault(p => p.Id == id.Value) ?? new Product();
                    var productViewModel = _mapper.Map(product, new ProductModel());

                    return productViewModel;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.InnerException?.Message ?? ex?.Message);
            }

            return new ProductModel();
        }

        public bool UpdateProduct(ProductModel model)
        {
            try
            {
                var productEntity = _dbContext.Products.FirstOrDefault(p => p.Id == model.Id);
                if (productEntity != null)
                {
                    _mapper.Map(model, productEntity);
                    _dbContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.InnerException?.Message ?? ex?.Message);
            }
            return false;
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    _dbContext.Products.Remove(product);
                    _dbContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex?.InnerException?.Message ?? ex?.Message);
            }

            return false;
        }

        public ShoppingCartModel GetAllProductsToSell()
        {
            var shoppingCart = ShoppingCartProducts();
            shoppingCart.ProductsToSell = _dbContext.Products.Where(p => p.ProductQuantity > 0).Select(p =>
               new ProductToSellModel
               {
                   ProductId = p.Id,
                   ProductName = p.ProductName,
                   ProductPrice = p.ProductPrice
               }).ToList();

            return shoppingCart;
        }

        public ShoppingCartModel AddProductsToSellToShoppingCart(ProductToSellModel productToSell)
        {
            var shoppingCart = _memoryCache.Get<ShoppingCartModel>("ShoppingCart") ?? new ShoppingCartModel();

            shoppingCart.ShoppingCartProducts.Add(new ProductToSellModel
            {
                ProductId = productToSell.ProductId,
                ProductName = productToSell.ProductName,
                ProductPrice = productToSell.ProductPrice
            });
            shoppingCart.TotalCost = shoppingCart.ShoppingCartProducts.Select(s => s.ProductPrice).Sum();

            ChacheData("ShoppingCart", shoppingCart);

            return shoppingCart;
        }

        public ShoppingCartModel ShoppingCartProducts()
        {
            var shoppingCart = _memoryCache.Get<ShoppingCartModel>("ShoppingCart") ?? new ShoppingCartModel();

            return shoppingCart;
        }

        public bool DeleteProductFromShoppingCart(int productId)
        {
            var shoppingCart = _memoryCache.Get<ShoppingCartModel>("ShoppingCart") ?? new ShoppingCartModel();

            if (shoppingCart.ShoppingCartProducts != null && shoppingCart.ShoppingCartProducts.Any())
            {
                var product = shoppingCart.ShoppingCartProducts.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    shoppingCart.TotalCost -= product.ProductPrice;
                    shoppingCart.ShoppingCartProducts.Remove(product);
                    ChacheData("ShoppingCart", shoppingCart);

                    return true;
                }
            }

            return false;
        }

        private void ChacheData(string key, object data)
        {
            var cacheOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(2));
            _memoryCache.Set(key, data, cacheOption);
        }

        public bool SaveOrder()
        {
            var shoppingCart = ShoppingCartProducts();
            if (shoppingCart != null && shoppingCart.ShoppingCartProducts != null && shoppingCart.ShoppingCartProducts.Any())
            {
                var order = new Order()
                {
                    OrderGuid = Guid.NewGuid(),
                    DateCreated = DateTime.UtcNow,
                    TotalCost = shoppingCart.TotalCost
                };

                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();

                var orderProductsList = new List<OrderProduct>();
                foreach (var item in shoppingCart.ShoppingCartProducts)
                {
                    orderProductsList.Add(new OrderProduct()
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId
                    });
                }

                _dbContext.OrderProducts.AddRange(orderProductsList);
                _dbContext.SaveChanges();

                _memoryCache.Remove("ShoppingCart");
                return true;
            }

            return false;
        }

        public List<OrderViewModel> GetAllOrders()
            => _dbContext.Orders.AsNoTracking().Select(o => new OrderViewModel
            {
                DateCreated = o.DateCreated.ToString("dddd, MMMM d, yyyy h:mm:ss tt"),
                TotalCost = o.TotalCost
            }).ToList() ?? new List<OrderViewModel>();
    }
}
