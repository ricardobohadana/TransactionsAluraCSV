using Bogus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using TransactionsAluraCSV.Domain.Entities;
using TransactionsAluraCSV.Domain.Interfaces.Services;
using Xunit;

namespace TransactionsAluraCSV.Tests
{
    public class UsersTests
    {
        private readonly IUserService _userService;

        public UsersTests(IUserService userService)
        {
            _userService = userService;
        }

        private HttpClient Initialize()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var webBuilder = new WebHostBuilder();
            webBuilder.UseConfiguration(configuration).UseStartup<Startup>();

            return new TestServer(webBuilder).CreateClient();
        }


        [Fact(DisplayName = "Test user creation")]
        public void CreateUser()
        {
            var faker = new Faker("pt_BR");
            User user = new()
            {
                Email = faker.Person.Email,
                Name = faker.Person.FirstName
            };
            _userService.Register(user);
        }
    }
}