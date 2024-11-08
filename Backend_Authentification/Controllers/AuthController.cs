using Microsoft.AspNetCore.Mvc;
using Backend_Authentification.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Backend_Authentification.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Chemain du fichier de stockage des utilisateurs
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Backend_Authentification", "Data", "UserAccount.json");
        private List<UserLoginModel> LoadUsers()
        {
            if (!System.IO.File.Exists(_filePath))
            {
                return new List<UserLoginModel>();
            }

            var json = System.IO.File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<UserLoginModel>>(json) ?? new List<UserLoginModel>();
        }
        private void SaveUsers(List<UserLoginModel> users)
        {
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            System.IO.File.WriteAllText(_filePath, json);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel loginModel)
        {
            var users = LoadUsers();
            var user = users.FirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password);

            if (user == null)
            {
                // 401 Unauthorized
                return Unauthorized(new {message = "Nom d'utilisateur ou le mot de passe incorrect"});
            }
            // 200 OK
            return Ok(new {message = "Connexion réussie" });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterModel registerModel)
        {
            var users = LoadUsers();

            // Vérifie si l'utilisateur existe déjà
            if (users.Any(u => u.Email == registerModel.Email))
            {
                return BadRequest(new { message = "L'utilisateur existe déjà." });
            }

            // Ajoute le nouvel utilisateur à la liste et sauvegarde
            users.Add(new UserLoginModel
            {
                Email = registerModel.Email,
                Password = registerModel.Password,
                PhoneNumber = registerModel.PhoneNumber,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName
            });

            SaveUsers(users);

            return Ok(new { message = "Compte créé avec succès" });
        }
    }
}
