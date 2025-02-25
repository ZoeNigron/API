using ApiNORDev.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiNORDev.Data
{
    public static class SeedData
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            // On crée un scope pour pouvoir utiliser l'injection de dépendances
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApiNORDevContext>();

            // Vérifie si la base de données contient déjà des utilisateurs
            if (context.Utilisateurs.Any())
            {
                return;
            }

            // On ajoute des utilisateurs à la base de données
            var utilisateur1 = new Utilisateur
            {
                Nom = "Nigron",
                Prenom = "Zoé",
                Email = "znigron@ensc.fr",
                PasswordHash = "1234",
            };

            // Ajout des utilisateurs au DbSet
            context.Utilisateurs.AddRange(utilisateur1);

            // Sauvegarde les modifications dans la base de données
            context.SaveChanges();
        }
    }
}
