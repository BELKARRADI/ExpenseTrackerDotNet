using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, string userType)
        {
            // Check if the user exists
            var user = GetUserByUsername(username);

            // Vérifiez le type d'utilisateur
            if (user != null)
            {
                // Verify the password
                if (password == user.Password)
                {
                    // Authentication successful
                    // Check the user's role and redirect accordingly
                    switch (user.Role)
                    {
                        case "user":
                            return RedirectToAction("Index", "Dashboard");
                        case "admin":
                            return RedirectToAction("Index", "Admin");
                        default:
                            return RedirectToAction("Index", "Login");
                    }
                }
            }

            // Si le type d'utilisateur est invalide, rediriger vers une page d'erreur ou afficher un message
            return RedirectToAction("Index", "Login");
        }

        public User? GetUserByUsername(string username)
        {
            // Retrieve the user by username from the database
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                return user;
            }
            else
                return null;
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Login");
        }

    }
}
