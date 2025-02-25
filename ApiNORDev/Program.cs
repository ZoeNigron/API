using ApiNORDev.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration du DbContext avec la chaîne de connexion depuis appsettings.json
builder.Services.AddDbContext<ApiNORDevContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuration de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

// Configuration de CORS pour autoriser les demandes venant de React
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

// Construire l'application
var app = builder.Build();

// Appliquer les migrations et initialiser la base de données
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApiNORDevContext>();

    context.Database.Migrate(); // Applique les migrations

    // Initialisation des données avec SeedData
    SeedData.Init(services); // Passe le IServiceProvider à Init
}

// Configurer Swagger en mode développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();

// Démarrer l'application
app.Run();
