using Microsoft.EntityFrameworkCore;
using CoAlert.Domain.Interfaces;
using CoAlert.Application.Interfaces;
using CoAlert.Application.Services;
using CoAlert.Infrastructure.Data.AppData;
using CoAlert.Infrastructure.Data.Repositories;
using DotNetEnv;

if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ORACLE_HOST")))
{
    DotNetEnv.Env.Load();
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactNativeApp", policy =>
    {                   
        policy.WithOrigins("http://localhost:8081")
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
    var oracleServiceName = Environment.GetEnvironmentVariable("ORACLE_SERVICE_NAME");
    var oracleSid = Environment.GetEnvironmentVariable("ORACLE_SID");

    if (!string.IsNullOrEmpty(oracleUser) && !string.IsNullOrEmpty(oraclePassword))
    {
        string connectDataPart;

        if (!string.IsNullOrEmpty(oracleServiceName))
        {
            // Usa SERVICE_NAME se estiver configurado
            connectDataPart = $"(SERVICE_NAME={oracleServiceName})";
        }
        else if (!string.IsNullOrEmpty(oracleSid))
        {
            // Usa SID se SERVICE_NAME não estiver definido mas SID estiver
            connectDataPart = $"(SID={oracleSid})";
        }
        else
        {
            // Pode definir um padrão, se quiser (exemplo: XE)
            connectDataPart = "(SID=XE)";
        }

        connectionString = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={oracleHost})(PORT={oraclePort})))(CONNECT_DATA=(SERVER=DEDICATED){connectDataPart}));User Id={oracleUser};Password={oraclePassword};";
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

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ILikeRepository, LikeRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<ICategoriaDesastreRepository, CategoriaDesastreRepository>();
builder.Services.AddTransient<ILocalizacaoRepository, LocalizacaoRepository>();
builder.Services.AddTransient<IPostagemRepository, PostagemRepository>();
builder.Services.AddTransient<IComentarioRepository, ComentarioRepository>();

builder.Services.AddTransient<ILikeApplicationService, LikeApplicationService>();
builder.Services.AddTransient<IUsuarioApplicationService, UsuarioApplicationService>();
builder.Services.AddTransient<ICategoriaDesastreApplicationService, CategoriaDesastreApplicationService>();
builder.Services.AddTransient<ILocalizacaoApplicationService, LocalizacaoApplicationService>();
builder.Services.AddTransient<IPostagemApplicationService, PostagemApplicationService>();
builder.Services.AddTransient<IComentarioApplicationService, ComentarioApplicationService>();

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

app.UseCors("AllowReactNativeApp");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();