using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WQLIdentityServerAPI.Configurations.Consts;

namespace WQLIdentityServerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = PolicyConst.Manager, AuthenticationSchemes = "Bearer")]
    public class TestValueController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ServiceDto>> Get()
        {
            var temp = this;
            var temp1 = User.IsInRole("admin");
            var temp2 = User.IsInRole("Administrator");
            return new ServiceDto[] {
            new ServiceDto() {
                Name ="Development",
                Uri="https://readify.net/services/development/",
                IconUri="https://readify.net/media/1187/development.png"
            },
            new ServiceDto() {
                Name ="Innovation and Design",
                Uri="https://readify.net/services/innovation-design/",
                IconUri="https://readify.net/media/1189/light-bulb.png"
            },
            new ServiceDto() {
                Name ="Data and Analytics",
                Uri="https://readify.net/services/data-analytics/",
                IconUri="https://readify.net/media/1184/data.png"
            },
            new ServiceDto() {
                Name ="DevOps",
                Uri="https://readify.net/services/devops/",
                IconUri="https://readify.net/media/1188/devops.png"
            },
        };
        }
        [HttpGet]
        public IActionResult Getuser()
        {
            var user = User.Claims.Select(c =>
          new
          {
              Type = c.Type,
              Value = c.Value
          });
            return Ok(user);

        }
    }
    public class ServiceDto
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string IconUri { get; set; }
    }
}