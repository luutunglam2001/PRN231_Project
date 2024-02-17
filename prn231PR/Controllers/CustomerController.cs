using DataAccsess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace prn231PR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (var context = new QLMTCContext())
            {
                var ls_cus = context.Customers.ToList();
                return Ok(ls_cus);
            }
        }

        [HttpGet("GetCustomerById")]
        public IActionResult GetCustomerById(int id)
        {
            using (var context = new QLMTCContext())
            {
                var cus = context.Customers.Where(a => a.CustomerId == id).FirstOrDefault();
                return Ok(cus);
            }
        }

        [HttpPost]
        public IActionResult Post(Customer p)
        {
            using (var context = new QLMTCContext())
            {
                context.Customers.Add(p);
                context.SaveChanges();
                return Ok(p);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            using (var context = new QLMTCContext())
            {
                var cus = context.Customers.Where(a => a.CustomerId == id).FirstOrDefault();
                if (cus == null)
                {
                    return NotFound();
                }
                var accounts = context.Accounts.Where(a => a.CustomerId == id).ToList();
                var orders = context.Orders.Where(a => a.CustomerId == id).ToList();
                foreach (var order in orders)
                {
                    var order_details = context.OrderDetails.Where(a => a.OrderId == order.OrderId).ToList();
                    foreach (var odt in order_details)
                    {
                        context.OrderDetails.Remove(odt);
                    }
                    
                }

                context.Accounts.RemoveRange(accounts);
                context.Orders.RemoveRange(orders);
                context.Customers.Remove(cus);
                context.SaveChanges();
                return Ok(cus);
            }
        }

        [HttpPut]
        public IActionResult Put(Customer cus)
        {
            using (var context = new QLMTCContext())
            {
                var p = context.Customers.Find(cus.CustomerId);
                if (p == null) return NotFound();
                p.CompanyName = cus.CompanyName;
                p.Address = cus.Address;
                p.ContactName = cus.ContactName;
                p.ContactTitle = cus.ContactTitle;
                context.Update(p);
                context.SaveChanges();
                return Ok(p);
            }
        }
    }
}
