using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabProject.Controllers.V3
{
    [Authorize]
    [ApiController]
    [ApiVersion("3.0")]
    [Route("api/v3/[controller]")]
    [ApiExplorerSettings(GroupName = "v3")]
    public class TestController : Controller
    {
        private readonly IMyService _myService;

        public TestController(IMyService myService)
        {
            _myService = myService;
        }

        [HttpGet]
        public IActionResult MyMethod_v3()
        {
            _myService.GetExcelFile();
            var response = new BaseResponse<string>()
            {
                Description = "Created",
                StatusCode = 200
            };
            return Ok(response);
        }
    }
}