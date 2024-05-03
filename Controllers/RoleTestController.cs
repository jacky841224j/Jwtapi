using Jwtapi.Enum;
using Jwtapi.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Jwtapi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoleTestController : Controller
    {
        [HttpGet]
        [Role(UserTypes.Admin)]
        public string Admin()
        {
            return "呼叫成功";
        }

        [HttpGet]
        [Role(UserTypes.User)]
        public string User()
        {
            return "呼叫成功";
        }

        [HttpGet]
        [Role(UserTypes.None)]
        public string None()
        {
            return "呼叫成功";
        }
    }
}
