using DataAccsess.Models;
using DataAccsess.Respository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace prn231PR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private CartIRespository respository = new CartRespository();

        //[HttpGet]
        //public IActionResult GetProductCart()
        //{
        //    try
        //    {
        //        var getall = respository.getItemsInCart();
        //        if (getall == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(getall);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }

        //}

        [HttpGet]
        //[Route("GetProductCartById/{id}")]
        public IActionResult GetProductCartById(int id)
        {
            try
            {
                var getall = respository.getProductCart(id);
                if (getall == null)
                {
                    return NotFound();
                }
                return Ok(getall);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        [HttpDelete("deleteCart")]
        public IActionResult deleteCart(int id)
        {
            try
            {
                respository.deleteProductCart(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            try
            {

                bool check = respository.Delete(id);
                if (!check)
                {
                    return NotFound();
                }
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("getallOrder")]
        public IActionResult GetOrder()
        {
            try
            {
                var getall = respository.getALLOrder();
                if (getall == null)
                {
                    return NotFound();
                }
                return Ok(getall);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
   
        [HttpPost("creatOrder")]
        public IActionResult Post(Order book)
        {
            try
            {
                respository.addorders(book);
                return Ok(book);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 

        }

        [HttpPost("createListOrderDetail")]
        public IActionResult addOrderDetail(List<OrderDetail> orderDetails)
        {
            try
            {
                foreach(var orderDetail in orderDetails)
                {
                    respository.addOrderDetail(orderDetail);
                }
                
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("[action]/{UserId:int}/{ProductId:int}")]
        public IActionResult RemoveFromCart(int UserId, int ProductId)
        {
            try
            {
                Order order = respository.GetAllOrdersByMember(UserId).FirstOrDefault();
                respository.deleteCart(order.OrderId, ProductId);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok();


        }
    }
}
