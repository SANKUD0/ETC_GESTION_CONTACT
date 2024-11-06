using ETC_GESTION_CONTACT.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETC_GESTION_CONTACT.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            // Logique de connexion ici
            return RedirectToAction("Index", "GestionCompte");
        }
    }
}
