using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TravelBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookFlightController : ControllerBase
    {
        private readonly IBookFlightBL bookflightBL;

        public BookFlightController(IBookFlightBL bookflightBL)
        {
            this.bookflightBL = bookflightBL;

        }
        [Authorize]
        [HttpPost]
        [Route("BookFlight")]
        public ActionResult BookFlight(BookFlightModel bookflight)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = this.bookflightBL.BookFlight(bookflight, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Book Flight Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Book Flight failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("CancelFlight")]
        public ActionResult CancelFlight(string id)
        {
            try
            {
                string userid = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = bookflightBL.CancelFlight(id);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Flight canceled Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Flight canceled failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
