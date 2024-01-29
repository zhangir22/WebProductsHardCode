using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProductsHardCode.Data;
using WebProductsHardCode.Models.CategoryModels;
namespace WebProductsHardCode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly DbHardCodeContext _db;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ILogger<CategoryController> logger)
        {
            _db = new DbHardCodeContext();
            _logger = logger;
        }
        [HttpGet("GetCategories")]
        public async Task<List<Category>?> GetCategoriesAsync()
        {
            var val = await _db.Categories.ToListAsync();
            if (val != null)
                if (val.Count > 0)
                    return val;
            _logger.LogDebug("Category list is not full");
            return null;
        }
        [HttpGet("GetCategoryById/{id}")]
        public async Task<Category?> GetCategoryByIdAsync(int? id)
        {
            if(id != null)
                if(id.HasValue)
                    return await _db.Categories.FirstOrDefaultAsync(p => p.Id == id);
            _logger.LogDebug($"Category by {id} is not");
            return null;
        }
        [HttpPost("AddCategory")]
        public async void AddCategoryAsync([FromBody] Category? category)
        {
            if (category != null)
                if(!_db.Categories.Contains(category))
                    await _db.Categories.AddAsync(category);
            _logger.LogDebug("Category cant add");
            await _db.SaveChangesAsync();
        }
        [HttpDelete("DeleteCategory/{id}")]
        public async void DeleteAsync(int? id)
        {
            if(id != null)
                if(id.HasValue)
                    _db.Categories.Remove(await GetCategoryByIdAsync(id));
            _logger.LogDebug($"Cant be deleted {id}");
            await _db.SaveChangesAsync();
        }
    }
}
