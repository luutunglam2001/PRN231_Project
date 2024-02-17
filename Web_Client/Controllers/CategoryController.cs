using DataAccsess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Web_Client.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        public List<Category> categories { get; set; } = new List<Category>();
        public CategoryController() {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7162/api/Product";
        }
       /* public async Task<IActionResult> listCate()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync(ProductApiUrl + "/getAllCate");
                // HttpResponseMessage responseMessage = await client.GetAsync(ProductApiUrl);
                using (HttpContent content = res.Content)
                {
                    string strdata = await content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(strdata);
                    return View(categories);
                }
            }
        }*/
    }
}
