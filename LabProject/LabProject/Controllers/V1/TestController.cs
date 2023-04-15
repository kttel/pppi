using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabProject.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TestController : Controller
    {
        private readonly IMyService _myService;

        public TestController(IMyService myService)
        {
            _myService = myService;
        }

        [HttpGet]
        [Obsolete("This method is deprecated in version 1.0. Please use the updated version.")]
        public IActionResult MyMethod_v1()
        {
            int result = _myService.GetIntValue();
            return Ok(result);
        }
    }
}