using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AwsWebApi.Controllers
{    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> IndexAsync(string name)
        {
            return Ok($"test controller {name}!");
        }
    }
}