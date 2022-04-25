using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Domain.Interfaces.Services;
using TransactionsAluraCSV.Presentation.Models;

namespace TransactionsAluraCSV.Presentation.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            UserIndexModel model = new()
            {
                Users = _userService.GetUsers(),
            };
            return View(model);
        }

        
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (Guid.Parse(HttpContext.User.Identity.Name) == id)
                {
                    TempData["MensagemErro"] = "Você não pode excluir o próprio usuário.";
                    return RedirectToAction("Index");
                }

                _userService.DeleteUser(id);


                TempData["MensagemSucesso"] = "Usuário excluído com sucesso";

            }catch (Exception e)
            {
                TempData["MensagemErro"] = "O usuário não existe";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Por favor, confira os dados inseridos";
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
                TempData["MensagemErro"] = "Ocorreu um erro:" + e.Message;
            }

            return View();
        }

        public IActionResult Edit(Guid id)
        {
            User user;
            try
            {
                user = _userService.GetUser(id);
                UserEditModel model = new()
                {
                    Email = user.Email,
                    Name = user.Name,
                    UserId = user.UserId,
                };

                return View(model);
            }

            catch (Exception e)
            {

                TempData["MensagemErro"] = "Ocorreu um erro: " + e.Message;

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(UserEditModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MensagemErro"] = "Verifique os dados enviados e tente novamente.";
                return View(model);
            }
            try
            {
                User user = _userService.GetUser(model.UserId);
                user.Email = model.Email;
                user.Name = model.Name;

                _userService.UpdateUser(user);

                TempData["MensagemSucesso"] = "As alterações foram salvas com sucesso!";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Ocorreu um erro: " + e.Message;
                return View(model);
            }

            return View();
        }
    }
}
