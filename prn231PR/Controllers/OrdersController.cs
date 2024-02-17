using AutoMapper;
using DataAccsess.DTO;
using DataAccsess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace prn231PR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IMapper _mapper;
        public OrdersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetOrderDetailsByMonthYear/{month}/{year}")]
        public IActionResult GetOrderDetailsByMonthYear(int month, int year)
        {
            using (var context = new QLMTCContext())
            {
                DateTime now = DateTime.Now;
                int year1, month1;
                if (month == 0)
                {
                    var ls_odt = context.OrderDetails
                        .Where(a => a.Order.OrderDate.Value.Year == year).ToList();
                    var ls_odtDTO = _mapper.Map<List<OrderDetailDTO>>(ls_odt);
                    foreach (var odt in ls_odtDTO)
                    {
                        var order = context.Orders.Where(o => o.OrderId == odt.OrderId).Include(o => o.Customer).FirstOrDefault();
                        odt.CompanyName = order.Customer.CompanyName;
                        var pro = context.Products.Where(p => p.ProductId == odt.ProductId).FirstOrDefault();
                        odt.ProductName = pro.ProductName;
                    }
                    return Ok(ls_odtDTO);
                }
                else
                {
                    month1 = (int)month;
                    int daysInMonth = DateTime.DaysInMonth(year, month1);
                    DateTime firstDayOfMonth = new DateTime(year, month1, 1);
                    DateTime lastDayOfMonth = new DateTime(year, month1, daysInMonth);
                    var ls_odt = context.OrderDetails
                        .Where(a => a.Order.OrderDate <= lastDayOfMonth &&
                        a.Order.OrderDate >= firstDayOfMonth).ToList();
                    var ls_odtDTO = _mapper.Map<List<OrderDetailDTO>>(ls_odt);
                    foreach (var odt in ls_odtDTO)
                    {
                        var order = context.Orders.Where(o => o.OrderId == odt.OrderId).Include(o => o.Customer).FirstOrDefault();
                        odt.CompanyName = order.Customer.CompanyName;
                        var pro = context.Products.Where(p => p.ProductId == odt.ProductId).FirstOrDefault();
                        odt.ProductName = pro.ProductName;
                    }
                    return Ok(ls_odtDTO);
                }
                //else
                //{
                //    month1 = (int)month;
                //}
                //if (year == null)
                //{
                //    year1 = now.Month;
                //}
                //else
                //{
                //    year1 = (int)year;
                //}

                
                //decimal money = 0;
                //foreach(var a in ls_odt)
                //{
                //    money += a.Quantity* a.UnitPrice;
                //}
                
            }
        }

        [HttpGet]
        [Route("GetOrderDetailsByDateTime/{day}/{month}/{year}")]
        public IActionResult GetOrderDetailsByDateTime(int day, int month,int year)
        {
            using (var context = new QLMTCContext())
            {
                DateTime date = new DateTime(year, month, day);
                var ls_odt = context.OrderDetails
                    .Where(a => a.Order.OrderDate.Value.Date == date.Date).ToList();
                var ls_odtDTO = _mapper.Map<List<OrderDetailDTO>>(ls_odt);
                foreach (var odt in ls_odtDTO)
                {
                    var order = context.Orders.Where(o => o.OrderId == odt.OrderId).Include(o => o.Customer).FirstOrDefault();
                    odt.CompanyName = order.Customer.CompanyName;
                    var pro = context.Products.Where(p => p.ProductId == odt.ProductId).FirstOrDefault();
                    odt.ProductName = pro.ProductName;
                }
                return Ok(ls_odtDTO);
            }
        }

        [HttpGet]
        [Route("GetAllOrderDetails")]
        public IActionResult GetAllOrderDetails()
        {
            using (var context = new QLMTCContext())
            {
                var ls_odt = context.OrderDetails.ToList();
                var ls_odtDTO = _mapper.Map<List<OrderDetailDTO>>(ls_odt);
                foreach (var odt in ls_odtDTO)
                {
                    var order = context.Orders.Where(o => o.OrderId == odt.OrderId).Include(o => o.Customer).FirstOrDefault();
                    odt.CompanyName = order.Customer.CompanyName;
                    var pro = context.Products.Where(p => p.ProductId == odt.ProductId).FirstOrDefault();
                    odt.ProductName = pro.ProductName;
                }
                return Ok(ls_odtDTO);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            using (var context = new QLMTCContext())
            {
                var ls_pro = context.Orders.ToList();
                return Ok(ls_pro);
            }
        }

        [HttpGet("GetOrderById")]
        public IActionResult GetOrderById(int id)
        {
            using (var context = new QLMTCContext())
            {
                var cus = context.Orders.Where(a => a.OrderId == id).FirstOrDefault();
                return Ok(cus);
            }
        }

        [HttpPost]
        public IActionResult Post(Order p)
        {
            using (var context = new QLMTCContext())
            {
                context.Orders.Add(p);
                context.SaveChanges();
                return Ok(p);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int orderId)
        {
            using (var context = new QLMTCContext())
            {
                var order = context.Orders.Where(a => a.OrderId == orderId).FirstOrDefault();
                if (order == null)
                {
                    return NotFound();
                }
                var order_details = context.OrderDetails.Where(a => a.OrderId == orderId).ToList();
                foreach (var odt in order_details)
                {
                    context.OrderDetails.Remove(odt);
                }
                context.Orders.Remove(order);
                context.SaveChanges();
                return Ok(order);
            }
        }

        [HttpPut]
        public IActionResult Put(Order order)
        {
            using (var context = new QLMTCContext())
            {
                var p = context.Orders.Find(order.OrderId);
                if (p == null) return NotFound();
                p.CustomerId = order.CustomerId;
                p.OrderDate = order.OrderDate;
                p.RequiredDate = order.RequiredDate;
                p.ShippedDate = order.ShippedDate;
                p.Freight = order.Freight;
                p.ShipName = order.ShipName;
                p.ShipAddress = order.ShipAddress;
                p.ShipCity = order.ShipCity;
                p.ShipRegion = order.ShipRegion;
                p.ShipPostalCode = order.ShipPostalCode;
                p.ShipCountry = order.ShipCountry;
                context.Update(p);
                context.SaveChanges();
                return Ok(p);
            }
        }
    }
}
