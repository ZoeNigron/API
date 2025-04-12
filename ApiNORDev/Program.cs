// Ce fichier configure l'application ASP.NET Core en ajoutant les services nécessaires comme le DbContext, Swagger, CORS et les contrôleurs
// Il applique également les migrations de base de données et initialise les données lors du démarrage de l'application

using ApiNORDev.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// pour configurer le DbContext avec la chaîne de connexion depuis appsettings.json
builder.Services.AddDbContext<ApiNORDevContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// pour ajouter les services Swagger et les contrôleurs avant de builder l'application
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); // pour permettre la documentation du Swagger
});

builder.Services.AddControllers();

// pour configurer CORS pour autoriser les demandes venant de React
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:3000",
                    "http://172.20.10.2:3000",
                    "http://192.168.1.101:3000",
                    "https://nordev.netlify.app",
                    "http://192.168.0.42:3000"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// pour appliquer les migrations et initialiser la base de données
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApiNORDevContext>();

    context.Database.Migrate();

    // initialisation des données avec SeedData
    SeedData.Init(services);
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

// pour démarrer l'application
app.Run("http://0.0.0.0:5039");
