using DataAccsess.Models;
using DataAccsess.Respository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace prn231PR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserIRespository respository = new UserRespository();

        [HttpGet]
        public IActionResult GetUser(string email, string pass)
        {
            try
            {
                var data = respository.GetUserByEmailPass(email, pass);
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
        [HttpPost("register")]
        public IActionResult RegisteUser(Account user)
        {
            try
            {
                var dt = respository.registerUser(user);
                if (dt == null)
                {
                    return NotFound();
                }
                return Ok(dt);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

