using AzureFileUpload.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzureFileUpload.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Username == "Admin" && model.Password == "12345")
                {
                    HttpContext.Session.SetString("Username", model.Username);
                    return RedirectToAction("Index", "Pdf");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }
    }
}
