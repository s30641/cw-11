using Microsoft.EntityFrameworkCore;
using Cwiczenia11.Data;
using Cwiczenia11.Services;

var builder = WebApplication.CreateBuilder(args);

// Dodaj kontrolery do usług
builder.Services.AddControllers();

// Konfiguracja DbContext z connection string z appsettings.json
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

// Rejestracja serwisu DbService do wstrzykiwania zależności
builder.Services.AddScoped<IDbService, DbService>();

var app = builder.Build();

// Middleware autoryzacji
app.UseAuthorization();

// Mapowanie endpointów kontrolerów
app.MapControllers();

app.Run();