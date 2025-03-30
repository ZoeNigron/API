using ApiNORDev.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration du DbContext avec la chaîne de connexion depuis appsettings.json
builder.Services.AddDbContext<ApiNORDevContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Ajouter les services Swagger et les contrôleurs AVANT de builder l'application
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Configuration de CORS pour autoriser les demandes venant de React
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000", "http://172.20.10.2:3000")
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

// Appliquer les migrations et initialiser la base de données
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApiNORDevContext>();

    context.Database.Migrate(); // Applique les migrations

    // Initialisation des données avec SeedData
    SeedData.Init(services); // Passe le IServiceProvider à Init
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

// Démarrer l'application
app.Run("http://0.0.0.0:5039");

/*using ApiNORDev.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 📌 Configuration de la base de données avec la chaîne de connexion
builder.Services.AddDbContext<ApiNORDevContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 📌 Ajouter les services de l'API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 📌 Configurer Swagger avec une version valide d'OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API NORDev", Version = "v1" });
    c.EnableAnnotations(); // Permet d’utiliser des annotations Swagger
});

// 📌 Construire l'application
var app = builder.Build();

// 📌 Appliquer les migrations et initialiser la base de données
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApiNORDevContext>();

    context.Database.Migrate(); // Appliquer les migrations

    SeedData.Init(services); // Initialisation des données
}

// 📌 Activer Swagger uniquement en mode développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 📌 Middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 📌 Lancer l'application
app.Run();*/
