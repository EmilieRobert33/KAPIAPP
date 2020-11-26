using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EmailService;
using KAPIAPP.Services.DTO;
using KAPIAPP.Services.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KAPIAPP_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationDto userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var user = _mapper.Map<User>(userModel);

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userModel);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);

            var message = new Message(new string[] { user.Email }, "Email de confirmation", confirmationLink, null);
            await _emailSender.SendEmailAsync(message);
            await _userManager.AddToRoleAsync(user, "Evaluateur");

            return RedirectToAction(nameof(SuccessRegistration));

        }

        [HttpGet]
        public async Task<IActionResult> SuccessRegistration()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDto userModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var result = await _signInManager
                .PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(LoginTwoStep), new { userModel.Email, userModel.RememberMe, returnUrl });
            }
            if (result.IsLockedOut)
            {
                var forgotPassLink = Url.Action(nameof(ForgotPassword), "Account", new { }, Request.Scheme);
                var content = string.Format($"Votre compte est bloqué, pour redéfinir votre mot de passe cliquez sur ce lien {forgotPassLink}");

                var message = new Message(new string[] { userModel.Email }, "compte bloqué", content, null);
                await _emailSender.SendEmailAsync(message);

                ModelState.AddModelError("", "Le compte est bloqué");
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Erreur lors de la tentative de connexion");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPassword);
            }

            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "votre mail n'existe pas dans notre base");
                return View();
            }
            else
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callback = Url.Action(nameof(ResetPassword), "Account",
                                        new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { "emilie.robert05@gmail.com" },
                                                        "Redéfinir le mot de passe",
                                                        callback, null);
                await _emailSender.SendEmailAsync(message);

                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDto { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPassword);
            }

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user == null)
            {
                RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);

            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoginTwoStep(string email, bool rememberMe, string returnUrl = null)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View(nameof(Error));

            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if (!providers.Contains("Email"))
            {
                return View(nameof(Error));
            }

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            var message = new Message(new string[] { email }, "A2F code d'authentification", token, null);
            await _emailSender.SendEmailAsync(message);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginTwoStep(TwoStepDto twoStepModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(twoStepModel);

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
                return RedirectToAction(nameof(Error));

            var result = await _signInManager.TwoFactorSignInAsync("Email", twoStepModel.TwoFactorCode, twoStepModel.RememberMe, rememberClient: false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                var forgotPassLink = Url.Action(nameof(ForgotPassword), "Account", new { }, Request.Scheme);
                var content = string.Format($"Votre compte est bloqué, pour redéfinir votre mot de passe cliquez sur ce lien {forgotPassLink}");

                var message = new Message(new string[] { user.Email }, "compte bloqué", content, null);
                await _emailSender.SendEmailAsync(message);

                ModelState.AddModelError("", "Le compte est bloqué");
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Erreur lors de la tentative de connexion");
                return View();
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}