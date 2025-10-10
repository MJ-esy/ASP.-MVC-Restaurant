using ASP.MVC.Models;
using ASP.MVC.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ASP.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _client;
        public AdminController(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("ASP_Reservations");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginAdmin)
        {
            var response = await _client.PostAsJsonAsync("Auth/login", loginAdmin);
            if (!response.IsSuccessStatusCode)
            {
                return View(loginAdmin);
            }

            //check for token response and save in token response
            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            var jwt = tokenResponse.Token;

            //create cookie to save in client side
            var handler = new JwtSecurityTokenHandler();
            var jwtObject = handler.ReadJwtToken(jwt);
            HttpContext.Response.Cookies.Append("jwtToken", jwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = jwtObject.ValidTo
            });

            var claims = jwtObject.Claims.ToList();
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                claimsPrincipal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = jwtObject.ValidTo
                });
          
            return RedirectToAction("AdminPage", "Admin");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("jwtToken");
            return RedirectToAction("Login", "Admin");
        }

        [Authorize]
        public async Task<IActionResult> AdminPage()
        {
            var token = HttpContext.Request.Cookies["jwtToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Admin");
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return View();
        }


      
    }
}
