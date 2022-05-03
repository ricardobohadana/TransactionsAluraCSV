using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TransactionsAluraCSV.Domain.Interfaces.Mail;
using TransactionsAluraCSV.Domain.Interfaces.Repositories;
using TransactionsAluraCSV.Domain.Interfaces.Services;
using TransactionsAluraCSV.Domain.Services;
using TransactionsAluraCSV.Domain.Utils;
using TransactionsAluraCSV.Infra.Data.Contexts;
using TransactionsAluraCSV.Infra.Data.Repositories;

namespace TransactionsAluraCSV.Tests
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(Assembly.Load("TransactionAluraCSV.Presentation")).AddControllersAsServices();
            // leitura da connectionstring
            string connectionString = Configuration.GetConnectionString("Postgres");

            //configuração da classe SqlServecContext do projeto Infra.Data para funcionamento correto do Entity Framework
            services.AddDbContext<PostgreSqlContext>(options => options.UseNpgsql(connectionString));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITransferRepository, TransferRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITransferService, TransferService>();
            services.AddTransient<IMailProvider, MailProvider>();

            services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options => {
                //     Controls how much time the authentication ticket stored in the cookie will remain
                //     valid from the point it is created The expiration information is stored in the
                //     protected cookie ticket.
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                //     The SlidingExpiration is set to true to instruct the handler to re-issue a new
                //     cookie with a new expiration time any time it processes a request which is more
                //     than halfway through the expiration window.
                options.SlidingExpiration = true;
                //     The AccessDeniedPath property is used by the handler for the redirection target
                //     when handling ForbidAsync.
                options.AccessDeniedPath = "/Forbidden/";
                }
            );

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
