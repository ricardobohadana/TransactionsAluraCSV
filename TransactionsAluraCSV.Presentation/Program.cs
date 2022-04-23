using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TransactionsAluraCSV.Domain.Interfaces.Mail;
using TransactionsAluraCSV.Domain.Interfaces.Repositories;
using TransactionsAluraCSV.Domain.Interfaces.Services;
using TransactionsAluraCSV.Domain.Services;
using TransactionsAluraCSV.Domain.Utils;
using TransactionsAluraCSV.Infra.Data.Contexts;
using TransactionsAluraCSV.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Working with sessions
builder.Services.AddDistributedMemoryCache();

// Working with Entity Framework Core
bool isDev = true;
string connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<PostgreSqlContext>(_ => _.UseNpgsql(connectionString));

// injeção de dependência dos repositórios
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITransferRepository, TransferRepository>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITransferService, TransferService>();
builder.Services.AddTransient<IMailProvider, MailProvider>();

//else
//{
//    string conn = builder.Configuration.GetConnectionString("BDPostgresSQL");
//    Uri databaseUri = new Uri(conn);
//    var userInfo = databaseUri.UserInfo.Split(':');
//    var npgsqlBuilder = new NpgsqlConnectionStringBuilder
//    {
//        Host = databaseUri.Host,
//        Port = databaseUri.Port,
//        Username = userInfo[0],
//        Password = userInfo[1],
//        Database = databaseUri.LocalPath.TrimStart('/'),
//        SslMode = SslMode.Require,
//        TrustServerCertificate = true,
//    };

//    connectionString = npgsqlBuilder.ToString();

//}


// Habilitando o projeto para usar cookies e autenticação de acesso
builder.Services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//autenticação e autorização
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
