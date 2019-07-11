using Microsoft.AspNetCore.Mvc;

namespace purplewellfaeries.Controllers
{
  public class WishController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}