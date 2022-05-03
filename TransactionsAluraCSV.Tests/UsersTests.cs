using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Xunit;

namespace TransactionsAluraCSV.Tests
{
    public class UsersTests
    {
        private HttpClient Initialize()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var webBuilder = new WebHostBuilder();
            webBuilder.UseConfiguration(configuration).UseStartup<Startup>();

            return new TestServer(webBuilder).CreateClient();
        }


        [Fact(DisplayName = "Test number 1")]
        public void Test1()
        {

        }
    }
}