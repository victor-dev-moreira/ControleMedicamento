// Objetivo: Rodar um servidor web
// Servidor Web: um programa que executa na rede local/remota


// Objeto de configuracão do servidor
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Habilita o MVC = Model - View - Controller
builder.Services.AddControllersWithViews();


WebApplication app = builder.Build();

// Middlewares - funções que executam à cada requisição e resposta
app.UseRouting();
app.MapDefaultControllerRoute();


// Executa o servidor
app.Run();
