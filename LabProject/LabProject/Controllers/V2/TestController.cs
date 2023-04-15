using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabProject.Controllers.V2
{
    [Authorize]
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v2/[controller]")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class TestController : Controller
    {
        private readonly IMyService _myService;

        public TestController(IMyService myService)
        {
            _myService = myService;
        }

        [HttpGet]
        public IActionResult MyMethod_v2()
        {
            string result = _myService.GetTextValue();
            return Ok(result);
        }
    }
}