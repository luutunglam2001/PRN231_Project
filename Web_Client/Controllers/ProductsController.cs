using DataAccsess.Helper;
using DataAccsess.Models;
using DataAccsess.Respository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Web_Client.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private ProductIRespository respository = new ProductRespository();
        public List<Category> categories { get; set; } = new List<Category>();
        private readonly QLMTCContext context = new QLMTCContext();
        public List<Product> products = new List<Product>();
        public int datac { get; set; }
        public List<Item> cart;


        public ProductsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7162/api/Product";
        }
        public async Task<IActionResult> ListProduct(string? searchpname, int? cateId,int? page)
        {
          cart = SessionHelper.GetObjectFormJson<List<Item>>(HttpContext.Session, "cart");
            if(cart != null)
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i] != null)
                    {
                        int count = 1;
                        count += i;
                        ViewData["count"] = count;
                    }
                }
                
            }
            if(cart == null)
            {
                RedirectToAction("ListProduct", "Products");
            }
          
            int pageSize = 8;
            if(page == null)
            {
                page = 1;
            }
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync(ProductApiUrl + "/getAllCate");
                // HttpResponseMessage responseMessage = await client.GetAsync(ProductApiUrl);
                using (HttpContent content = res.Content)
                {
                    string strdata = await content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(strdata);
                    ViewData["category"] = categories;
                    ViewData["datac"] = cateId == null ? -1 : cateId;
                }
            }
            if (cateId != null)
            {
                List<Product> products = respository.getProductByCate(cateId);
             
                List<Product> pros = products.Skip(((int)page - 1) * pageSize).Take(pageSize).ToList();
                int totalPages = 0;
                if (products.Count() % pageSize == 0)
                {
                    totalPages = products.Count() / pageSize;
                }
                else
                {
                    totalPages = products.Count() / pageSize + 1;
                }
                ViewBag.CategoryId = cateId;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                return View(pros);
              
            }
            if (searchpname != null )
            {
                using (HttpClient client = new HttpClient())
                {
                    //https://localhost:7162/api/Product/idNameSearch?pnames=adad
                    using (HttpResponseMessage res = await client.GetAsync(ProductApiUrl + "/idNameSearch?pnames=" + searchpname))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            //   Console.WriteLine($"{data}");
                            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(data);
                            List<Product> pros = products.Skip(((int)page - 1) * pageSize).Take(pageSize).ToList();
                            if (searchpname != null)
                            {
                                ViewData["searchpname"] = searchpname;
                            }
                            int totalPages = 0;
                            if (products.Count() % pageSize == 0)
                            {
                                totalPages = products.Count() / pageSize;
                            }
                            else
                            {
                                totalPages = products.Count() / pageSize + 1;
                            }

                            ViewBag.CurrentPage = page;
                            ViewBag.TotalPages = totalPages;
                            return View(pros);
                        }

                    }

                }
            }
            else
            {
                
                HttpResponseMessage responseMessage = await client.GetAsync(ProductApiUrl);
                // HttpResponseMessage responseMessage = await client.GetAsync(ProductApiUrl);

                string strdata = await responseMessage.Content.ReadAsStringAsync();

                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(strdata);
                List<Product> pros = products.Skip(((int)page - 1) * pageSize).Take(pageSize).ToList();
                int totalPages = 0;
                if (products.Count() % pageSize == 0)
                {
                    totalPages = products.Count() / pageSize ;
                }else
                {
                    totalPages = products.Count() / pageSize + 1;
                }
                
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                return View(pros);
            }
        }
       

        public void OnPost(int? cateId)
        {
            string getall = Request.Form["all"];
            if (cateId == null)
            {
                products = context.Products.Include(p => p.Category).ToList();
               // products = context.Products.Where(x => x.Category.CategoryId == cateId).ToList();
                ViewData["getall"] = products;
            }
            else
            {
               
                products = context.Products
                    .Include(p => p.Category)
                    .Where(p => p.CategoryId == cateId)
                    .ToList();
                datac = (int)cateId;
            }
        }
    }
}
