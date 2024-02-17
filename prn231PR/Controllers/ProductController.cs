using DataAccsess.Models;
using DataAccsess.Respository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace prn231PR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductIRespository respository = new ProductRespository();
        [HttpGet]
        public IActionResult GetALlProductt() {
            try
            {
                var getall = respository.GetAllProduct();
                if (getall == null)
                {
                    return NotFound();
                }
                return Ok(getall);

            } catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        [HttpGet("idNameSearch")]
        public IActionResult SearchAll(string? pnames)
        {
            try
            {
                var data = respository.getBuyIdName(pnames);
                ;
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getProByCate")]
        public IActionResult GetPByCate(int? cateid)
        {
            try
            {
                var data = respository.getProductByCate(cateid);
                ;
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllCate")]
        public IActionResult getAllcate()
        {
            try
            {
                var data = respository.getAllCate();
                ;
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetProductById")]
        public IActionResult GetProductById(int id)
        {
            using (var context = new QLMTCContext())
            {
                var pro = context.Products.Where(a => a.ProductId == id).FirstOrDefault();
                return Ok(pro);
            }

        }

        [HttpPost]
        public IActionResult Post(Product p)
        {
            using (var context = new QLMTCContext())
            {
                context.Products.Add(p);
                context.SaveChanges();
                return Ok(p);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            using (var context = new QLMTCContext())
            {
                var product = context.Products.Where(a => a.ProductId == id).FirstOrDefault();
                if (product == null)
                {
                    return NotFound();
                }
                var order_details = context.OrderDetails.Where(a => a.ProductId == id).ToList();
                foreach (var odt in order_details)
                {
                    context.OrderDetails.Remove(odt);
                }
                context.Products.Remove(product);
                context.SaveChanges();
                return Ok(product);
            }
        }

        [HttpPut]
        public IActionResult Put(Product pro)
        {
            using (var context = new QLMTCContext())
            {
                var p = context.Products.Find(pro.ProductId);
                if (p == null) return NotFound();
                p.ProductName = pro.ProductName;
                p.Picture = pro.Picture;
                p.UnitPrice = pro.UnitPrice;
                p.QuantityPerUnit = pro.QuantityPerUnit;
                p.Quantity = pro.Quantity;
                p.CategoryId = pro.CategoryId;
                p.Desscription = pro.Desscription;
                p.UnitsInStock = pro.UnitsInStock;
                p.Discontinued = pro.Discontinued;
                context.Update(p);
                context.SaveChanges();
                return Ok(p);
            }
        }
    }
}
