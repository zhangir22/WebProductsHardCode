using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProductsHardCode.Data;
using WebProductsHardCode.Models.ProductModels; 
namespace WebProductsHardCode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DbHardCodeContext _db;
        private readonly ILogger<CategoryController> _logger;
        public ProductController(ILogger<CategoryController> logger)
        {
            _db = new DbHardCodeContext();
            _logger = logger;
        }
      
        [HttpGet("GetProduct")] 
        public async Task<List<Product>?> GetProductsAsync()
        {
            if(_db.Products.Count() > 0)
                return await _db.Products.ToListAsync();
            _logger.LogDebug("List is not full");
            return null;

        }
        [HttpGet("GetProductsByCategory/{id}")]
        public async Task<IEnumerable<Product>?> GetProductsByCategoryIdAsync(int id)
        {
            var val = await _db.Products.Where(p => p.CategoryId == id).ToListAsync();
            if (val.Count > 0)
                return val;
            _logger.LogDebug("Category List is not full");
            return null;
        }
        [HttpGet("GetProductById/{id}")]
        public async Task<Product?> GetProductByIdAsync(int? id)
        { 
            if(id != null)
                if(id.HasValue)
                {
                    var val = await _db.Products.FirstOrDefaultAsync(p=> p .Id == id);
                    if(val != null)
                        return val;
                }
            _logger.LogDebug("Proudct by id is not database");
            return null;
        }
        [HttpPost("AddProduct")]
        public async void AddProductAsync ([FromBody]Product? product) 
        {
            if(product != null)
                await _db.Products.AddAsync(product);
            _logger.LogDebug("Product is null");
            await _db.SaveChangesAsync();
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async void DeleteAsync(int? id)
        {
            if(id != null)
                if(id.HasValue)
                    _db.Remove(GetProductByIdAsync(id));
            _logger.LogDebug("Product cant be deleted");
            await _db.SaveChangesAsync();
        }
    }
}
