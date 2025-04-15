using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProject.Models;
using System.Text.Json;

namespace RazorProject.Pages
{
    public class LoginModel : PageModel
    {
        public string? ErrorMessage { get; set; }

        public IActionResult OnPost(string username, string password)
        {
            var usersFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "users.json");
            if (!System.IO.File.Exists(usersFilePath))
            {
                ErrorMessage = "User data file not found.";
                return Page();
            }

            var usersJson = System.IO.File.ReadAllText(usersFilePath);
            var users = JsonSerializer.Deserialize<List<User>>(usersJson);

            if (users == null || !users.Any())
            {
                ErrorMessage = "No users found.";
                return Page();
            }

            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password && u.IsActive);
            if (user == null)
            {
                ErrorMessage = "Invalid credentials or inactive user.";
                return Page();
            }

            // Generate a simple token
            var token = Guid.NewGuid().ToString();

            // Store values in session
            HttpContext.Session.SetString("username", username);
            HttpContext.Session.SetString("token", token);
            HttpContext.Session.SetString("session_id", HttpContext.Session.Id);

            // Store values in cookies
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(30),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("username", username, cookieOptions);
            Response.Cookies.Append("token", token, cookieOptions);
            Response.Cookies.Append("session_id", HttpContext.Session.Id, cookieOptions);

            // Redirect to the table page
            return RedirectToPage("/Index");
        }
    }
}
