using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Додати конфігурацію ocelot.json
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Зареєструвати Ocelot
builder.Services.AddOcelot();

var app = builder.Build();

// Запустити Ocelot middleware
await app.UseOcelot();

app.Run();
