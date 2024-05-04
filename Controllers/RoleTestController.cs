using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jwtapi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoleTestController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public string Admin()
        {
            return "呼叫成功";
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public string User()
        {
            return "呼叫成功";
        }

        [HttpGet]
        [Authorize(Roles = "None")]
        public string None()
        {
            return "呼叫成功";
        }
    }
}
