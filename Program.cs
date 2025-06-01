using Microsoft.EntityFrameworkCore;
using CoAlert.Domain.Interfaces;
using CoAlert.Application.Interfaces;
using CoAlert.Application.Services;
using CoAlert.Infrastructure.Data.AppData;
using CoAlert.Infrastructure.Data.Repositories;
using DotNetEnv;
using Microsoft.Extensions.DependencyInjection;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {                   
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

string connectionString = builder.Configuration.GetConnectionString("Oracle");

if (string.IsNullOrWhiteSpace(connectionString))
{
    var oracleUser = Environment.GetEnvironmentVariable("ORACLE_USER");
    var oraclePassword = Environment.GetEnvironmentVariable("ORACLE_PASSWORD");
    var oracleHost = Environment.GetEnvironmentVariable("ORACLE_HOST") ?? "localhost";
    var oraclePort = Environment.GetEnvironmentVariable("ORACLE_PORT") ?? "1521";
    var oracleSid = Environment.GetEnvironmentVariable("ORACLE_SID") ?? "xe";

    if (!string.IsNullOrEmpty(oracleUser) && !string.IsNullOrEmpty(oraclePassword))
    {
        connectionString = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={oracleHost})(PORT={oraclePort})))(CONNECT_DATA=(SERVER=DEDICATED)(SID={oracleSid})));User Id={oracleUser};Password={oraclePassword};";
    }
    else
    {
        throw new Exception("Nenhuma string de conexão Oracle configurada e variáveis de ambiente insuficientes.");
    }
}

builder.Services.AddDbContext<ApplicationContext>(x =>
{
    x.UseOracle(connectionString);
});

// Add MVC services
builder.Services.AddControllersWithViews();

// Repository Dependencies
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<ICategoriaDesastreRepository, CategoriaDesastreRepository>();
builder.Services.AddTransient<ILocalizacaoRepository, LocalizacaoRepository>();
builder.Services.AddTransient<IPostagemRepository, PostagemRepository>();
builder.Services.AddTransient<IComentarioRepository, ComentarioRepository>();
builder.Services.AddTransient<ILikeRepository, LikeRepository>();

// Application Service Dependencies
builder.Services.AddTransient<IUsuarioApplicationService, UsuarioApplicationService>();
builder.Services.AddTransient<ICategoriaDesastreApplicationService, CategoriaDesastreApplicationService>();
builder.Services.AddTransient<ILocalizacaoApplicationService, LocalizacaoApplicationService>();
builder.Services.AddTransient<IPostagemApplicationService, PostagemApplicationService>();
builder.Services.AddTransient<IComentarioApplicationService, ComentarioApplicationService>();
builder.Services.AddTransient<ILikeApplicationService, LikeApplicationService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseCors("AllowReactApp");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); 