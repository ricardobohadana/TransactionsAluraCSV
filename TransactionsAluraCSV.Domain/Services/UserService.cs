using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Domain.Interfaces.Mail;
using TransactionsAluraCSV.Domain.Interfaces.Repositories;
using TransactionsAluraCSV.Domain.Interfaces.Services;
using TransactionsAluraCSV.Domain.Utils;

namespace TransactionsAluraCSV.Domain.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailProvider _mailProvider;

        public UserService(IUserRepository userRepository, IMailProvider mailProvider)
        {
            _userRepository = userRepository;
            _mailProvider = mailProvider;
        }

        public void Register(User user)
        {

            if (_userRepository.FindByEmail(user.Email) != null) throw new Exception("Este email já está cadastrado");

            string password = PasswordGenerator.GenerateSixDigitPassword();
            if (user.Email == "admin@email.com.br" && user.Name == "Admin")
            {
                password = "123999";
            }
            string encryptedPassword = EncryptString.MD5Hash(password);
            user.Password = encryptedPassword;

            _mailProvider.SendPassword(user.Email, password);

            _userRepository.Insert(user);
        }

        public User LogIn(string email, string password)
        {
            string encryptedPassword = EncryptString.MD5Hash(password);
            User user = _userRepository.FindByCredentials(email, encryptedPassword);

            if (user == null) throw new Exception("Email ou senha incorretos.");

            return user;
        }

        // mostra apenas usres que não foram logicamente excluídos
        public List<User> GetUsers()
        {
            List<User> users = _userRepository.GetAll();
            return users.FindAll(u => u.show == true);
        }

        // exclusão lógica
        public void DeleteUser(Guid id)
        {
            User user = _userRepository.GetById(id);
            user.show = false;
            _userRepository.Update(user);
        }

        public User GetUser(Guid id)
        {
            User user = _userRepository.GetById(id);
            if (user == null) throw new Exception("Usuário não encontrado.");
            return user;
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
    }
}
