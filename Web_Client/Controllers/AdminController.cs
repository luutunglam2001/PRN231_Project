using DataAccsess.DTO;
using DataAccsess.Models;
using DataAccsess.Respository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;

namespace Web_Client.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        public List<OrderDetailDTO> month { get; set; } = new List<OrderDetailDTO>();
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7162/api/Orders";
        }

        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage res = await client.GetAsync(ProductApiUrl+"/GetOrderDetailsByMonthYear"+"/"+DateTime.Now.Month
                    +"/"+DateTime.Now.Year);

                using (HttpContent content = res.Content)
                {
                    string strdata = await content.ReadAsStringAsync();
                    month = JsonConvert.DeserializeObject<List<OrderDetailDTO>>(strdata);
                    ViewData["Month"] = month;

                    // SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                    
                }
                HttpResponseMessage res1 = await client.GetAsync(ProductApiUrl+"/GetOrderDetailsByMonthYear"+"/"+0
                    +"/"+DateTime.Now.Year);

                using (HttpContent content = res1.Content)
                {
                    string strdata = await content.ReadAsStringAsync();
                    month = JsonConvert.DeserializeObject<List<OrderDetailDTO>>(strdata);
                    ViewData["Year"] = month;

                    // SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                }
                HttpResponseMessage res2 = await client.GetAsync(ProductApiUrl+"/GetOrderDetailsByDateTime"
                    +"/"+DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year);

                using (HttpContent content = res2.Content)
                {
                    string strdata = await content.ReadAsStringAsync();
                    month = JsonConvert.DeserializeObject<List<OrderDetailDTO>>(strdata);
                    ViewData["Date"] = month;

                    // SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                }
                HttpResponseMessage res3 = await client.GetAsync(ProductApiUrl+"/GetAllOrderDetails");

                using (HttpContent content = res3.Content)
                {
                    string strdata = await content.ReadAsStringAsync();
                    month = JsonConvert.DeserializeObject<List<OrderDetailDTO>>(strdata);
                    ViewData["Total"] = month;

                    // SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                }
            }
            return View("Index");
        }

        public async Task<IActionResult> ProductAdmin(string? search_name,int? page)
        {
            if(page == null)
            {
                page = 1;
            }
            int pageSize = 5;
            HttpResponseMessage res = await client.GetAsync("https://localhost:7162/api/Product" + "/getAllCate");
            // HttpResponseMessage responseMessage = await client.GetAsync(ProductApiUrl);
            using (HttpContent content = res.Content)
            {
                string strdata = await content.ReadAsStringAsync();
                List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(strdata);
                ViewData["category"] = categories;
            }
            

            HttpResponseMessage response = await client.GetAsync("https://localhost:7162/api/Product");
            string strData = await response.Content.ReadAsStringAsync();
            List<Product> items = null;
            List<Product> items1 = null;
            if (search_name != null)
            {
                items = JsonConvert.DeserializeObject<List<Product>>(strData).Where(a => a.ProductName.Contains(search_name)).ToList();
                items1 = JsonConvert.DeserializeObject<List<Product>>(strData).Where(a => a.ProductName.Contains(search_name))
                    .Skip(((int)page - 1) * pageSize).Take(pageSize).ToList()
                    .ToList();
                ViewData["searchpname"] = search_name;
            }
            else
            {
                items = JsonConvert.DeserializeObject<List<Product>>(strData);
                items1 = JsonConvert.DeserializeObject<List<Product>>(strData).Skip(((int)page - 1) * pageSize).Take(pageSize).ToList();
            }
            int totalPages = 0;
            if (items.Count() % pageSize == 0)
            {
                totalPages = items.Count() / pageSize;
            }
            else
            {
                totalPages = items.Count() / pageSize + 1;
            }
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(items1);
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(string product_name,int category_id,string quantity, string unit_price,string picture,string description)
        {
            int number;
            decimal number1;
            if (int.TryParse(unit_price,out number) == false){
                TempData["validation_quantity"] = "You must input correct type(int)";
            }
            if (decimal.TryParse(unit_price, out number1) == false)
            {
                TempData["validation_price"] = "You must input correct type(decimal)";
            }
            if(TempData["validation_quantity"] != null || TempData["validation_price"] != null)
            {
                return RedirectToAction("ProductAdmin");
            }
            Product pro = new Product
            {
                ProductName = product_name,
                CategoryId = category_id,
                Quantity = int.Parse(quantity),
                UnitPrice = decimal.Parse(unit_price),
                Picture = picture,
                Desscription = description
            };
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7162/api/Product", pro);
            return RedirectToAction("ProductAdmin");
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(int id)
        {
            string edit = "https://localhost:7162/api/Product"+"/GetProductById"+"?id="+id;
            HttpResponseMessage response = await client.GetAsync(edit);
            string strData = await response.Content.ReadAsStringAsync();
            Product book = JsonConvert.DeserializeObject<Product>(strData);

            HttpResponseMessage res = await client.GetAsync("https://localhost:7162/api/Product" + "/getAllCate");
            HttpContent content = res.Content; 
            string strdata = await content.ReadAsStringAsync();
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(strdata);
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(Product pro)
        {
            HttpResponseMessage res = await client.GetAsync("https://localhost:7162/api/Product" + "/getAllCate");
            HttpContent content = res.Content;
            string strdata = await content.ReadAsStringAsync();
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(strdata);
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            HttpResponseMessage response = await client.PutAsJsonAsync("https://localhost:7162/api/Product", pro);
            return RedirectToAction("ProductAdmin");
        }

        public async Task<IActionResult> ProductDelete(int id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync("https://localhost:7162/api/Product"+"?id="+id);
            return RedirectToAction("ProductAdmin");
        }

        public async Task<IActionResult> CustomerAdmin(string? search_name,int? page)
        {
            int pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            HttpResponseMessage response = await client.GetAsync("https://localhost:7162/api/Customer");
            string strData = await response.Content.ReadAsStringAsync();

            List<Customer> items = null;
            List<Customer> items1 = null;
            if (search_name != null)
            {
                items = JsonConvert.DeserializeObject<List<Customer>>(strData).Where(a => a.CompanyName.Contains(search_name)).ToList();
                items1 = JsonConvert.DeserializeObject<List<Customer>>(strData).Where(a => a.CompanyName.Contains(search_name))
                    .Skip(((int)page - 1) * pageSize).Take(pageSize)
                    .ToList();
                ViewData["searchpname"] = search_name;
            }
            else
            {
                items = JsonConvert.DeserializeObject<List<Customer>>(strData);
                items1 = JsonConvert.DeserializeObject<List<Customer>>(strData).Skip(((int)page - 1) * pageSize).Take(pageSize).ToList();
            }
            
            int totalPages = 0;
            if (items.Count() % pageSize == 0)
            {
                totalPages = items.Count() / pageSize;
            }
            else
            {
                totalPages = items.Count() / pageSize + 1;
            }
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(items1);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerCreate(string company_name,string contact_name,string contact_title,string address)
        {
            Customer pro = new Customer
            {
                CompanyName = company_name,
                ContactName = contact_name,
                Address = address,
                ContactTitle = contact_title
            };
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7162/api/Customer", pro);
            return RedirectToAction("CustomerAdmin");
        }

        [HttpGet]
        public async Task<IActionResult> CustomerEdit(int id)
        {
            string edit = "https://localhost:7162/api/Customer"+"/GetCustomerById"+"?id="+id;
            HttpResponseMessage response = await client.GetAsync(edit);
            string strData = await response.Content.ReadAsStringAsync();
            Customer book = JsonConvert.DeserializeObject<Customer>(strData);
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerEdit(Customer pro)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("https://localhost:7162/api/Customer", pro);
            return RedirectToAction("CustomerAdmin");
        }

        public async Task<IActionResult> CustomerDelete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync("https://localhost:7162/api/Customer"+"?id="+id);
            return RedirectToAction("CustomerAdmin");
        }
    }
}
