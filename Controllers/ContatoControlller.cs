using Microsoft.AspNetCore.Mvc;

namespace ApiMongoDB.Controllers
{
    public class ContatoControlller : Controller
    {
        // GETw
        public IActionResult Index()
        {
            return View();
        }
    }
}