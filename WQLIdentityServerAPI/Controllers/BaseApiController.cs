using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WQLIdentityServerAPI.Middleware.Exceptions;

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
            if (result)
            {
                return Ok(msg);
            }
            else
            {
                return BadRequest(new ErrorContent() { Message = "操作失败", StatusCode = 400 });
            }
        }


    }
}
