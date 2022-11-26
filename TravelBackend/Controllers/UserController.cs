using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TravelBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;

        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserRegisterModel register)
        {
            try
            {

                var result = userBL.Register(register);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Regestration Successfull", data = result });
                }
                else
                {

                    return BadRequest(new { success = false, message = "Regestration not Successfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        [HttpPost("Login")]

        public ActionResult Login(UserLoginModel login)
        {
            try
            {
                var result = userBL.Login(login);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login is  Succecsfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login is not Successfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        //[Authorize]
        [HttpPost("ForgotPassword")]
        public ActionResult FogotPassword(string Email)
        {
            try
            {
                var result = userBL.FogotPassword(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset link sent Succecsfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset link sending failed" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("ResetLink")]
        public ActionResult ResetLink(string password, string confirmPassword)
        {

            try
            {

                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();

                var result = userBL.ResetLink(Email, password, confirmPassword);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Password Changed SUCCESSFULL" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password Changed FAILED" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
