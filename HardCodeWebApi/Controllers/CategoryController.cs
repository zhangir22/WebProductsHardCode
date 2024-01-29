using HardCodeWebApi.Data;
using HardCodeWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DbHardcodeContext _db;
        public CategoryController(DbHardcodeContext db)
        {
            _db = db;
        }
        [HttpGet("GetCategories")]
        public async Task<IEnumerable<Category>?> GetCategoriesAsync() 
        {
            if(ModelState.IsValid)
            {
                if (_db != null)
                {
                    if(_db.Categories.Count() > 0)
                    {
                        return await _db.Categories.ToListAsync();
                    }
                }
            }
            return null;
        }
        [HttpGet("GetCategoryById/{id}")]
        public async Task<Category?> GetCategoryByIdAsync(int? id)
        {
            if (ModelState.IsValid)
            {
                if(_db != null)
                {
                    if(id.HasValue&&id > 0) 
                    {
                        var product = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
                        if(product != null)
                        {
                            return product;
                        }
                    }
                }
            }
            return null;
        }
        [HttpPost("AddNewCategory")]
        public async Task<IActionResult>AddCategoryAsync([FromBody] Category? category)
        {
            if (ModelState.IsValid)
            {
                if(category != null)
                {
                    if(!_db.Categories.Contains(await GetCategoryByIdAsync(category.Id)))
                    {
                        await _db.Categories.AddAsync(category);
                        await _db.SaveChangesAsync();
                        return Ok(category);
                    }
                    return BadRequest();
                }
                return NotFound(category);
            }
            return NoContent();
        }
        [HttpGet("DeleteCategory/{id}")]
        public async Task<IActionResult>DeleteCateogryAsync(int? id)
        {
            if(ModelState.IsValid)
            {
                if(id.HasValue && id > 0)
                {
                    var category = await GetCategoryByIdAsync(id);
                    if(category != null)
                    {
                        await _db.Products.Where(x => x.CategoryId == id).ExecuteUpdateAsync(s => s.SetProperty(p => p.CategoryId, p => p.CategoryId * 0));
                        _db.Categories.Remove(category);
                        await _db.SaveChangesAsync();
                        return Ok(category);
                    }
                    return BadRequest(nameof(Category));
                }
                return NotFound(id);
            }
            return NoContent();
        }
    }
}
