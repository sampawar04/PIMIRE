using Microsoft.AspNetCore.Mvc;
using PimireWebApp.Models;
using PimireWebApp.Utilities;

namespace PimireWebApp.Controllers
{
    public class AuthenticateController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            SessionHelper.RemoveObjectFromJson(HttpContext.Session, "UserProfile");
            LoginModel loginModel = new LoginModel();
            HttpContext.Request.Cookies.TryGetValue("UserName", out string userName);
            HttpContext.Request.Cookies.TryGetValue("Password", out string password);
            HttpContext.Request.Cookies.TryGetValue("IsRememberMe", out string isRemeberMe);
            loginModel.UserName = userName;
            loginModel.Password = password;
            loginModel.IsRememberMe = Convert.ToBoolean(isRemeberMe);
            return View(loginModel);
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.UserName) && string.IsNullOrEmpty(loginModel.Password))
            {
                TempData["ErrorMessage"] = "Please enter credential";
                return View(loginModel);
            }
            if (string.IsNullOrEmpty(loginModel.UserName))
            {
                TempData["ErrorMessage"] = "Please enter username";
                return View(loginModel);
            }
            
            if (string.IsNullOrEmpty(loginModel.Password))
            {
                TempData["ErrorMessage"] = "Please enter password";
                return View(loginModel);
            }
            if(loginModel.UserName == "virat.kohli@gmail.com" && loginModel.Password=="virat@123")
            {
                if (loginModel.IsRememberMe)
                {
                    CookieOptions option = new CookieOptions();
                    option.Secure = true;
                    option.Expires = DateTime.Now.AddDays(365);
                    HttpContext.Response.Cookies.Append("UserName", loginModel.UserName, option);
                    HttpContext.Response.Cookies.Append("Password", loginModel.Password, option);
                    HttpContext.Response.Cookies.Append("IsRememberMe", loginModel.IsRememberMe.ToString(), option);
                }
                else
                {
                    HttpContext.Response.Cookies.Delete("UserName");
                    HttpContext.Response.Cookies.Delete("Password");
                    HttpContext.Response.Cookies.Delete("IsRememberMe");
                }
                User user = new User
                {
                    Id = 18,
                    Name = "Virat Kohli",
                    MobileNumber = "123456789",
                    Email = "virat.kohli@gmail.com",
                    Address = "Delhi"
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "UserProfile", user);
                return RedirectToAction("product-list", "Shop");
            }
            else
                TempData["ErrorMessage"] = "Invalid credential";

            return View();
        }
    }
}
