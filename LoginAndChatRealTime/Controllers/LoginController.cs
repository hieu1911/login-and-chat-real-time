using LoginAndChatRealTime.Helper;
using LoginAndChatRealTime.Models;
using Microsoft.AspNetCore.Mvc;
using LoginAndChatRealTime.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using LoginAndChatRealTime.Interfaces;

namespace LoginAndChatRealTime.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserSerivce _userService;

        public LoginController(IUserSerivce userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(LoginViewModel loginViewModel)
        {
            using (var db = new MyDbContext()) 
            {
                var user = _userService.GetUser(loginViewModel.UserName, loginViewModel.Password);
                if (user is null) 
                {
                    return Redirect("/Login");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("Id", user.Id.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { };
                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }

            return Redirect("/");
        }
    }
}
