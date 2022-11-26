using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace TravelBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddStateController : ControllerBase
    {
        private readonly IAddStateBL stateBL;
        public AddStateController(IAddStateBL stateBL)
        {
            this.stateBL = stateBL;
        }
        [Authorize]
        [HttpPost("Addstate")]
        public ActionResult AddState(AddStateModel addstate)
        {
            try
            {
                string userid = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var state = this.stateBL.AddState(addstate,userid);
                if (state != null)
                {
                    return this.Ok(new { success = true, message = "state added Successfully", data = state });
                }
                return this.BadRequest(new { success = false, message = "state Already Exits", data = state });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllState")]
        public ActionResult GetallStates()
        {
            try
            {
                var state = this.stateBL.GetallStates();
                if (state != null)
                {
                    return this.Ok(new { success = true, message = "state Details Fetched Successfully", data = state });
                }
                return this.BadRequest(new { success = false, message = "State Details Could Not Be Fetched" });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("UploadImage")]
        public IActionResult UploadImage(string id, IFormFile img)
        {
            try
            {
                string userid = User.Claims.FirstOrDefault(e => e.Type == "UserId").Value;
                var result = stateBL.UploadImage(id, img, userid);
                if (result != null)
                {
                    return this.Ok(new { message = "uploaded ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Not uploaded" });
                }
            }
            catch (Exception)
            {
                //_logger.LogError(ex.ToString());
                throw;
            }
        }

    }
}
