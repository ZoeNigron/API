// La classe ApiNORDevContext représente le contexte de la base de données, en définissant les DbSet pour toutes les entités
// Elle configure également la chaîne de connexion SQLite et initialise la base de données si elle n'est pas déjà configurée

using ApiNORDev.Model;
using Microsoft.EntityFrameworkCore;

public class ApiNORDevContext : DbContext
{
    public DbSet<Astuce> Astuces { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Competence> Competences { get; set; }
    public DbSet<Exercice> Exercices { get; set; }
    public DbSet<Lecon> Lecons { get; set; }
    public DbSet<QuestionQuiz> QuestionsQuiz { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }

    public string? DbPath { get; private set; }

    public ApiNORDevContext(DbContextOptions<ApiNORDevContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            DbPath = "ApiNORDev.db";
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}
