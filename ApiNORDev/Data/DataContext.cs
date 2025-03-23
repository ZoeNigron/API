using ApiNORDev.Model;
using Microsoft.EntityFrameworkCore;

public class ApiNORDevContext : DbContext
{
    public DbSet<Astuce> Astuces { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }

    public ApiNORDevContext(DbContextOptions<ApiNORDevContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
