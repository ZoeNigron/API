using ApiNORDev.Controllers;
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

    public ApiNORDevContext(DbContextOptions<ApiNORDevContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
