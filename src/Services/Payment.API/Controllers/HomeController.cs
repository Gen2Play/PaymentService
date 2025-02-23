using Microsoft.AspNetCore.Mvc;

namespace Payment.API.Controllers
{
    public class HomeController : ControllerBase
    {
        // GET
        public IActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
