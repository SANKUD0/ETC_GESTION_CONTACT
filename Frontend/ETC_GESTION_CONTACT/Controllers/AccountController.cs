using ETC_GESTION_CONTACT.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETC_GESTION_CONTACT.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        public AccountController()
        {
            // Configure l'URL de base pour pointer vers le backend
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:32778");
        }

        // Affiche la page de connexion
        public IActionResult Login()
        {
            return View();
        }
        // Gère la soumission du formulaire de connexion
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            // Envoie la requête POST au backend avec les informations de connexion
            var responde = await _httpClient.PostAsJsonAsync("/api/auth/login", model);
            // Si la connexion est réussie, redirige vers la page d'accueil
            if (responde.IsSuccessStatusCode)
                return RedirectToAction("Home", "GestionContact");
            else
            {
                // Si la connexion échoue, affiche un message d'erreur
                ViewBag.Error = "Email ou mot de passe incorrect";
                return View(model);
            }
        }

        // Affiche la page d'inscription
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
             var response = await _httpClient.PostAsJsonAsync("/api/auth/register", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");
            else
            {
                ViewBag.Error = "Erreur lors de la création du compte";
                return View(model);
            }
        }
    }
}
