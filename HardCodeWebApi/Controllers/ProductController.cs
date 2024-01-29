using HardCodeWebApi.Data;
using HardCodeWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DbHardcodeContext _db;
        public ProductController(DbHardcodeContext db)
        {
            _db = db;
        }
        [HttpGet("GetProducts")]
        public async Task<IEnumerable<Product?>> GetProductsAsync()
        {
            if (ModelState.IsValid)
                if (_db.Products.Count() > 0)
                    return await _db.Products.ToListAsync();
            return null;
        }
        [HttpGet("GetProductById/{id}")]
        public async Task<Product?> GetProductByIdAsync(int? id)
        {
            if (ModelState.IsValid)
                if (id.HasValue && id.Value > 0)
                {
                    Product product = await _db.Products.SingleOrDefaultAsync(x => x.Id == id.Value);
                    if(product != null)
                        return product;
                }
            return null;
        }
        [HttpPost("AddNewProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product? product)
        {
            if (ModelState.IsValid)
            {
                if (product != null)
                    if (!_db.Products.Contains(product) )
                    {
                        await _db.Products.AddAsync(product);
                        await _db.SaveChangesAsync();
                        return Ok(product);
                    }
                return NotFound(product);
            }
            return NoContent();
        }
        [HttpDelete("DeletProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(ModelState.IsValid)
            {
                var product = await GetProductByIdAsync(id);
                if (id.HasValue && id.Value > 0)
                {
                    if(product!=null && _db.Products.Contains(product))
                    {
                        _db.Products.Remove(product);
                        await _db.SaveChangesAsync();
                        return Ok(product);
                    }
                    return NotFound(product);
                }
                return NotFound(id);
            }
            return NoContent();
        }
    }
}
