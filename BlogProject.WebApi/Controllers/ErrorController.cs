using BlogProject.Business.Tools.FacadeTool;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly IFacade _facade;

        public ErrorController(IFacade facade)
        {
            _facade = facade;
        }
        [HttpGet]
        public IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _facade.CustomLogger.LogError($"\nError place: {errorInfo.Path}\n Error message: {errorInfo.Error.Message}\n Stack trace: {errorInfo.Error.StackTrace}");
            return Problem(detail: "Please contact with developers.");
        }
    }
}
