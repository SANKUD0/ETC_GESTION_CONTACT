using Microsoft.AspNetCore.Mvc;

namespace ETC_GESTION_CONTACT.Controllers
{
    public class GestionContactController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
