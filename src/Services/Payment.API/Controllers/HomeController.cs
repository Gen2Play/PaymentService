using Microsoft.AspNetCore.Mvc;

namespace Payment.API.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
