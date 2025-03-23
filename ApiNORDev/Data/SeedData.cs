using System;
using System.Linq;
using ApiNORDev.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiNORDev.Data
{
    public static class SeedData
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApiNORDevContext>();

            if (!context.Utilisateurs.Any())
            {
                var utilisateur1 = new Utilisateur
                {
                    Nom = "Nigron",
                    Prenom = "Zoé",
                    Email = "znigron@ensc.fr",
                    MotDePasse = "1234",
                };

                context.Utilisateurs.Add(utilisateur1);
            }

            if (!context.Astuces.Any())
            {
                var astuces = new[]
                {
                    new Astuce { Contenu = "Pense à bien observer les bâtiments autour de toi !" },
                    new Astuce
                    {
                        Contenu =
                            "Utilise le soleil pour repérer les points cardinaux : il se lève à l’est et se couche à l’ouest.",
                    },
                    new Astuce
                    {
                        Contenu =
                            "Note des repères visuels marquants comme une statue ou un arbre particulier.",
                    },
                    new Astuce
                    {
                        Contenu =
                            "En marchant, essaie d’imaginer une carte mentale du trajet que tu empruntes.",
                    },
                    new Astuce
                    {
                        Contenu =
                            "Regarde souvent derrière toi pour mémoriser ton itinéraire sous un autre angle.",
                    },
                    new Astuce
                    {
                        Contenu =
                            "Écoute les sons environnants, comme une fontaine ou une route passante, pour te situer.",
                    },
                    new Astuce
                    {
                        Contenu =
                            "Teste ton sens de l’orientation en essayant de rejoindre un point sans GPS !",
                    },
                    new Astuce
                    {
                        Contenu =
                            "Apprends à lire une carte et entraîne-toi à suivre un itinéraire avec.",
                    },
                    new Astuce
                    {
                        Contenu =
                            "Fais attention aux noms des rues et aux numéros des bâtiments pour mieux te repérer.",
                    },
                    new Astuce
                    {
                        Contenu =
                            "Prends des points de repère naturels, comme une montagne ou une rivière, pour t’orienter.",
                    },
                };

                context.Astuces.AddRange(astuces);
            }

            context.SaveChanges();
        }
    }
}
