using DotNetEnv;
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

Env.Load();

// Working with Entity Framework Core
bool isDev = false;

var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
var api_key = Environment.GetEnvironmentVariable("EMAIL_API_KEY");
var api_secret = Environment.GetEnvironmentVariable("EMAIL_SECRET_KEY");

if (api_key == null || api_secret == null)
{
    throw new Exception("Problemas com as credenciais do email");
}

if (connectionString == null)
{
    connectionString = builder.Configuration.GetConnectionString("Postgres");
}

builder.Services.AddDbContext<PostgreSqlContext>(builder => builder.UseNpgsql(connectionString));

//get environment 
//builder.Configuration.AddEnvironmentVariables();
//var dotenv = Path.Combine(Directory.GetCurrentDirectory(), ".env");


// injeção de dependência dos repositórios
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITransferRepository, TransferRepository>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<ITransferService, TransferService>();
builder.Services.AddTransient<IMailProvider, MailProvider>(builder => new MailProvider(api_key, api_secret));

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

// Railway app

var portVar = Environment.GetEnvironmentVariable("PORT");
if (portVar is { Length: > 0 } && int.TryParse(portVar, out int port))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(port);
    });
}



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
    pattern: "{controller=Home}/{action=Index}"
);

app.Run();
