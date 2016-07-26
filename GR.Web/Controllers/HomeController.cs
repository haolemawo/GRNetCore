using System; 
using Microsoft.AspNetCore.Mvc;

namespace GR.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
