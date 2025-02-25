using ApiNORDev.Model;
using Microsoft.EntityFrameworkCore;

public class ApiNORDevContext : DbContext
{
    // Déclaration des DbSets pour les entités que tu veux gérer
    public DbSet<Utilisateur> Utilisateurs { get; set; }

    // Constructeur qui accepte DbContextOptions pour permettre l'injection de configuration
    public ApiNORDevContext(DbContextOptions<ApiNORDevContext> options)
        : base(options) { }

    // Si tu veux personnaliser la configuration du modèle, tu peux ajouter ici
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
