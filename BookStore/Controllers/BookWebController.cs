using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookWebController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
