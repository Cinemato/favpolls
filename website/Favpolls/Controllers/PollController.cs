using Microsoft.AspNetCore.Mvc;

namespace Favpolls.Controllers
{
    public class PollController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
