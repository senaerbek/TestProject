using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestProject.Captcha;
using TestProject.Email;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AccountController> logger;
        private readonly IEmailSender emailSender;
        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, ILogger<AccountController> logger, IEmailSender emailSender)
        {
            this.emailSender = emailSender;
            this.logger = logger;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var captchaResponse = Request.Form["g-Recaptcha-Response"];
            var isValid = GoogleCaptcha.Validate(captchaResponse);
            if (isValid)
            {
                var user = await userManager.FindByNameAsync(model.username);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ViewBag.Message = "Lütfen Ben Robot Değilim Seçeneğini İşaretleyin";
            }
            return View("Login", "Account");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new Users
            {
                UserName = model.username,
                EmailConfirmed = true
            };

            string[] roleNames = { "Admin", "Marka" };
            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    //dizideki roller yoksa ekle
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            var result = await userManager.CreateAsync(user, model.password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Marka");
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Register", "Account");
        }

        public IActionResult AccessDenied() => View();



        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid PAssword");
            }
            return View();
        }

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                return View("ResetPasswordConfirmation");
            }
            return View(model);
        }




        public IActionResult ForgotPasswordConfirmation() => View();

        [AllowAnonymous]
        [HttpGet("forgot-password")]
        public IActionResult ForgotPassword() => View();

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(Email);
                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                    new { email = Email, token = token }, Request.Scheme
                    );
                    logger.Log(LogLevel.Warning, passwordResetLink);
                    System.Console.WriteLine(passwordResetLink);
                    var message = new Message(new string[] { user.ToString() }, "Şifre Sıfırlama Linki", passwordResetLink);
                    emailSender.SendEmail(message);
                    System.Console.WriteLine(message);
                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }
            return View(Email);
        }

    }
}