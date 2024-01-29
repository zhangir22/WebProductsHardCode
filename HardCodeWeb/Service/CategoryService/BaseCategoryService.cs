using HardCodeWeb.Models;
using Newtonsoft.Json;
using System.Text;

namespace HardCodeWeb.Service.CategoryService
{
    public class BaseCategoryService
    {
        private readonly HttpClient _client;
        public BaseCategoryService(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<Category>> GetAllCategory()
        {
            var response =  await _client.GetAsync("api/Category/GetCategories");
            return JsonConvert.DeserializeObject<List<Category>>(await response.Content.ReadAsStringAsync()); 
        }

    }
   
}
