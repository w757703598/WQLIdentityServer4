using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WQLIdentityServerAPI.Middleware.Exceptions;
using WQLIdentityServerAPI.Models;

namespace WQLIdentityServerAPI.Controllers
{
    [ApiController]
    //[Authorize(Roles ="Administrator")]
    public class BaseApiController : ControllerBase
    {

        protected IEnumerable<string> ModelStateErrors
        {
            get
            {
                var erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return erros;
            }
        }
        protected IActionResult IdentityResponse<T>(IdentityResult result, T msg)
        {
            if (result.Succeeded)
            {
                return Ok(msg);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        protected IActionResult ResultResponse(bool result, string msg)
        {
            var content = new DefaultResponse<string>();
            if (result)
            {
                content.StatusCode = 0;
                content.Data = msg + "成功";
                content.Result = true;
                return Ok(content);
            }
            else
            {
                content.StatusCode = -1;
                content.Data = msg + "失败";
                content.Result = false;
                return BadRequest(content);
            }
        }


    }
}
