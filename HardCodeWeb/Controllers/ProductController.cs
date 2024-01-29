using HardCodeWeb.Models;
using HardCodeWeb.Service.CategoryService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace HardCodeWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _url = "https://localhost:7110/";
        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
        }
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("api/Product/GetProducts");
            var res = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());
            return View(res);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (ModelState.IsValid)
            {
                if(id.HasValue && id > 0)
                {
                    var val = await _client.GetAsync($"api/Product/GetProductById/{id}");
                    if(val != null)
                    {
                        return View(JsonConvert.DeserializeObject<Product>(await val.Content.ReadAsStringAsync()));
                    }
                    return BadRequest();
                }
                return NotFound(id);
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            BaseCategoryService service = new BaseCategoryService(_client);
            return View(service.GetAllCategory().Result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product? product)
        {
            if(ModelState.IsValid)
            {
                if(product != null)
                {
                    JObject val = JObject.Parse(JsonConvert.SerializeObject(product));
                    HttpContent content = new StringContent(val.ToString(), Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync("api/Product/AddNewProduct", content);
                    if(response.IsSuccessStatusCode)
                        return Redirect("Index");
                }
            }
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (ModelState.IsValid)
            {
                if(id.HasValue&&id.Value > 0)
                {
                    var response = _client.DeleteAsync($"api/DeletProduct/{id}");
                    if(response.IsCompleted)
                    {
                        return Redirect("Index");
                    }
                }
            }
            return View();
        }
    }
}
