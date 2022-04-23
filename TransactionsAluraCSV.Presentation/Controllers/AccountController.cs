using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Domain.Interfaces.Services;
using TransactionsAluraCSV.Presentation.Models;

namespace TransactionsAluraCSV.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Email ou senha inseridos não são válidos, por favor verifique.";
                ModelState.Clear();
                return View();
            }
            try
            {
                User user = _userService.LogIn(model.Email, model.Password);

                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.GivenName, user.Name),
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                };

                ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Transaction");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Ocorreu um erro: " + e.Message;
                return View();
            }

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Os dados preenchidos estão incorretos, por favor verifique!";
                ModelState.Clear();
                return View();
            }
            try
            {
                User user = new()
                {
                    UserId = Guid.NewGuid(),
                    Email = model.Email,
                    Name = model.Name,
                };
                _userService.Register(user);
                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso! Agora você já pode fazer login utilizando a senha que foi encaminhada para o seu email.";
                ModelState.Clear();
            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = e.Message;
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }
    }
}
