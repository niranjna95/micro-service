using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mongo.Web.Models;
using Mongo.Web.Models.Utility;
using Mongo.Web.Services.Interface;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mongo.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly ITokenProvider tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            this.authService = authService;
            this.tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestModel loginRequest = new();
            return View(loginRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
       {
            ResponseDto loginResponse = await authService.LoginAsync(model);
            
            if (loginResponse != null && loginResponse.IsSuccess)
            {
                LoginResponseModel loginResponseDto = JsonConvert.DeserializeObject<LoginResponseModel>(Convert.ToString(loginResponse.Result));
                await SignInUser(loginResponseDto);
                tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                TempData["error"] = loginResponse.Message;
                return View(model);
            }
        }
       [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>() {
                new SelectListItem {Text =SD.RoleAdmin, Value = SD.RoleAdmin },
                new SelectListItem {Text =SD.RoleCustomer, Value = SD.RoleCustomer }
            };
            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterationRequestModel model)
        {
            var roleList = new List<SelectListItem>() {
             new SelectListItem {Text =SD.RoleAdmin, Value = SD.RoleAdmin },
                    new SelectListItem {Text =SD.RoleCustomer, Value = SD.RoleCustomer }
            };
            ViewBag.RoleList = roleList;
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }else
            {
                ResponseDto registerResponse = await authService.RegisterAsync(model);
                ResponseDto assignRole;
                if (registerResponse != null && registerResponse.IsSuccess)
                {
                    if (string.IsNullOrEmpty(model.Role))
                    {
                        model.Role = SD.RoleCustomer;
                    }
                    assignRole = await authService.AssignRoleAsync(model);
                    if (assignRole != null && assignRole.IsSuccess)
                    {
                        TempData["success"] = "Register Succssfully";
                        return RedirectToAction(nameof(Login));
                    }
                }
                else
                {
                    TempData["error"] = registerResponse.Message;
                }
            }
           
           

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            tokenProvider.RemoveToken();
            return RedirectToAction("Index", "Home");
       
        }

        public async Task SignInUser(LoginResponseModel model)
        {

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim( JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type ==JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
        }
    }
}
