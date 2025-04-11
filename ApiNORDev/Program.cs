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
                .WithOrigins(
                    "http://localhost:3000",
                    "http://172.20.10.2:3000",
                    "http://192.168.1.101:3000",
                    "https://nordev.netlify.app"
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
