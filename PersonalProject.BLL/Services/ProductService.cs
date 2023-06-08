using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public ProductService(AppDbContext dbContext, ILogger<ProductService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public bool AddProduct(ProductViewModel model)
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

        public ProductViewModel GetProductById(int? id)
        {
            try
            {
                if (id.HasValue && id.Value != 0)
                {
                    var product = _dbContext.Products.AsNoTracking().FirstOrDefault(p => p.Id == id.Value) ?? new Product();
                    var productViewModel = _mapper.Map(product, new ProductViewModel());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.InnerException?.Message ?? ex?.Message);
            }

            return new ProductViewModel();
        }

        public bool UpdateProduct(ProductViewModel model)
        {
            try
            {
                var productEntity = _dbContext.Products.FirstOrDefault(p => p.Id == model.Id);
                if (productEntity != null)
                {
                    productEntity = _mapper.Map<Product>(model);

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
    }
}
