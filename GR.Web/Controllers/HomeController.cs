using System; 
using Microsoft.AspNetCore.Mvc;

namespace GR.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
