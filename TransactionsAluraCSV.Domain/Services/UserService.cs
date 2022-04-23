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
    }
}
