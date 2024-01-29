using HardCodeWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Policy;
using System.Text;

namespace HardCodeWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _url = "https://localhost:7110/";
        public CategoryController() 
        {
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(_url + "api/Category/GetCategories");
            if (response.IsSuccessStatusCode)
            {
                return View(JsonConvert.DeserializeObject<IEnumerable<Category>>(await response.Content.ReadAsStringAsync()));
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Category? category)
        {
            if (ModelState.IsValid)
            {
                if (category != null)
                {
                    JObject val = JObject.Parse(JsonConvert.SerializeObject(category));
                    HttpContent content = new StringContent(val.ToString(), Encoding.UTF8, "application/json");
                    var response =  await _client.PostAsync(_url + "api/Category/AddNewCategory", content);
                    if(response.IsSuccessStatusCode)
                    {
                        return Redirect("Index");
                    }
                }
                return NotFound(category);
            }
            return NoContent();
        }
  
   
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue && id.Value > 0)
                {
                    var response = await _client.GetAsync(_url + $"api/Category/DeleteCategory/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return Redirect("Index");
                    }
                }
            }
            return NoContent();
        }
    }
}
