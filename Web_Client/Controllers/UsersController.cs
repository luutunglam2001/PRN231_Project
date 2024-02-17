using DataAccsess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace Web_Client.Controllers
{
    public class UsersController : Controller
    {
        private string UserApiUrl = "";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly QLMTCContext context = new QLMTCContext();

        private const string ClientId = "1072499916375-4j8ab9ru243n21s8j1sjqjpj5n3oq39a.apps.googleusercontent.com";
        private const string RedirectUri = "http://localhost:5134";

        // https://localhost:7162/api/User?email=thopn3%40gmail.com&pass=12
        public UsersController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserApiUrl = "https://localhost:7162/api/User";
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                //ViewData["error"] = "Email and Password not empty!!!!";
                return View();
            }
            using (HttpClient client = new HttpClient())
            {
                // https://localhost:7162/api/User?email=thopn3%40gmail.com&pass=12222
                using (HttpResponseMessage res = await client.GetAsync(UserApiUrl + "?email=" + email + "&pass=" + password))
                {
                   
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        //   Console.WriteLine($"{data}"); 
                        Account user = JsonConvert.DeserializeObject<Account>(data);
                        if (user.AccountId == 0)
                        {
                            ViewData["message"] = "Do not have that account!";
                            return View();
                        }
                        else
                        {
                            if (user.Role == 0)
                            {
                                HttpContext.Session.SetInt32("role", user.Role);
                                HttpContext.Session.SetInt32("rolecate", (int)user.CustomerId);
                                HttpContext.Session.SetString("UserName", email);
                                return RedirectToAction("ListProduct", "Products");
                            }
                            else
                            {
                                HttpContext.Session.SetInt32("role", user.Role);
                                HttpContext.Session.SetString("UserName", email);
                                //tra ve trang admin
                                return RedirectToAction("Index", "Admin");
                            }

                        }
                    }
                }
                return View();
            }

        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("ListProduct","Products");
        }


        public async Task<IActionResult> Register(string email, string pass)
        {

            Account users = new Account();
            if (string.IsNullOrEmpty(email))
            {
                return View();
            }
          
            string pw = Request.Form["reqestPass"];
            if (pw != pass || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                HttpContext.Session.SetString("Message1", "Repass not match with pass or text empty!!");
                return RedirectToPage("/Register");
            }
            else
            {
              
               /* var cus = new Customer
                {
                    CompanyName = users.Usename,
                };
                context.Customers.Add(cus);
                context.SaveChanges();*/
              /* Customer customer = context.Customers.OrderBy(x => x.CustomerId).SingleOrDefault();
                users.CustomerId = customer.CustomerId;*/
               
                users.Usename = email;
                users.Password = pass;
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync(UserApiUrl + "/register", users))
                    {
                        using (HttpContent content = res.Content)
                        {
                            if (res.IsSuccessStatusCode)
                            {
                                HttpContext.Session.SetString("Message", "ADD SUCCESS");
                                Console.WriteLine("add succss");
                            }
                            else
                            {
                                HttpContext.Session.SetString("Message", "ADD FAIL");
                            }
                        }
                    }
                }
                return RedirectToAction("Login");
            }
        }
        public static async Task<string> GetAccessToken(string authorizationCode)
        {
            using (var client = new HttpClient())
            {
                var tokenEndpoint = "https://oauth2.googleapis.com/token";
                var requestBody = $"code={authorizationCode}&client_id={ClientId}&redirect_uri={RedirectUri}&grant_type=authorization_code";

                var response = await client.PostAsync(tokenEndpoint, new StringContent(requestBody));
                var responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
        }
    }
}
