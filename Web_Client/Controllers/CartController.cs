using DataAccsess.Helper;
using DataAccsess.Models;
using DataAccsess.Respository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace Web_Client.Controllers
{
    public class CartController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private CartIRespository respository = new CartRespository();
        public List<Item> cart { get; set; } = new List<Item>();
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public CartController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7162/api/Cart";
        }

        public async Task<IActionResult> Index(int id)
        {
           /* cart = SessionHelper.GetObjectFormJson<List<Item>>(HttpContext.Session, "cart");
            if(cart == null)
            {
                return RedirectToAction("ListProduct", "Products");
            }*/
         
            if (id == 0)
            {
                cart = SessionHelper.GetObjectFormJson<List<Item>>(HttpContext.Session, "cart");
                ViewData["getItem"] = cart;
                return View();
            }
            if (id != 0)
            {
                int count = 0;
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage res = await client.GetAsync(ProductApiUrl + "/?id=" + id);

                    using (HttpContent content = res.Content)
                    {
                        string strdata = await content.ReadAsStringAsync();
                        cart = JsonConvert.DeserializeObject<List<Item>>(strdata);
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                        ViewData["getItem"] = cart;
                       ViewData["count"] = count++;
                        if(cart == null)
                        {
                            return RedirectToAction("Index");
                        }
                        // SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                        return RedirectToAction("ListProduct","Products");
                    }
                }
            }
            else
            {
                HttpResponseMessage responseMessage = await client.GetAsync(ProductApiUrl);
                // HttpResponseMessage responseMessage = await client.GetAsync(ProductApiUrl);
                string strdata = await responseMessage.Content.ReadAsStringAsync();
                List<Order> products = JsonConvert.DeserializeObject<List<Order>>(strdata);
                return View(products);
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            if (Request.Form["ShipAddress"] == "" || Request.Form["ShipCountry"] == "" || Request.Form["ShippedDate"] == "")
            {
                ViewData["notnull"] = "Not empty text";
                return View();
            }
            Console.WriteLine("nhay vao day" + Request.Form["OrderDate"]);
            order.OrderDate = DateTime.Now;
            order.ShipAddress = Request.Form["ShipAddress"];
            order.ShipCountry = Request.Form["ShipCountry"];
            order.ShippedDate = DateTime.Parse(Request.Form["ShippedDate"]);
            order.ShipCity = "";
            order.ShipName = "";
            order.Freight = 10;

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            cart = SessionHelper.GetObjectFormJson<List<Item>>(HttpContext.Session, "cart");
            ViewData["getItem"] = cart;

            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.PostAsJsonAsync(ProductApiUrl + "/creatOrder", order);
            HttpContent content = res.Content;
            string strdata = await content.ReadAsStringAsync();
            Order order1 = JsonConvert.DeserializeObject<Order>(strdata);

            int order_id = order1.OrderId;
            foreach (var a in cart)
            {
                OrderDetail odt = new OrderDetail
                {
                    OrderId = order_id,
                    ProductId = a.Product.ProductId,
                    UnitPrice = (decimal)a.Product.UnitPrice,
                    Quantity = (short)a.Quantity,
                    Discount = 0

                };
                orderDetails.Add(odt);
            }
            HttpResponseMessage res1 = await client.PostAsJsonAsync(ProductApiUrl + "/createListOrderDetail", orderDetails);
            if (res.IsSuccessStatusCode)
            {
                
                HttpContext.Session.SetString("Message", "Update SUCCESS");
                HttpContext.Session.Remove("cart");
            }
            else
            {
                HttpContext.Session.SetString("Message", "Update Fail");
            }
           
            return RedirectToAction("ListProduct", "Products");
        }
        public async Task<IActionResult> RemoveFromCart(int ProductId)
        {
            
            HttpResponseMessage response = await client.GetAsync(
                $"Order/RemoveFromCart/{ProductId}");
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                //Tra ve thong bao loi
                ViewBag.Error = await response.Content.ReadAsStringAsync(); return BadRequest();
            }
            //Tra ve san pham da add
            // return URI of the created resource.
            return RedirectToAction("Index","Cart");//TO-DO: Dat Duong dan rang tra ve
        }
        public async Task<IActionResult> CreateOrder()
        {
            return View(); 
        }


        public async Task<IActionResult> Delete(int id)
        {

            cart = SessionHelper.GetObjectFormJson<List<Item>>(HttpContext.Session, "cart");
            //List<Item> product = new List<Item>();
            int index = cart.FindIndex(s => s.Product.ProductId == id);
            Console.WriteLine("_________ ");
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            //*//*  cart = SessionHelper.GetObjectFormJson<List<Item>>(HttpContext.Session, "cart");
            //  int index = Exits(cart, id);
            //cart.RemoveAt(index);
            //SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart); *//*
            return RedirectToAction("Index");
        }
        private int Exits(List<Item> cart, int id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
