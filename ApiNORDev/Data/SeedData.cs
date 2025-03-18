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

            // Ajouter des utilisateurs si ce n'est pas déjà fait
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

            // Ajouter des questions si ce n'est pas déjà fait
            if (!context.Questions.Any())
            {
                var question1 = new Question
                {
                    Contenu = "Pourquoi est-il important d'apprendre à évaluer les distances ?",
                    Reponses = new List<Reponse>
                    {
                        new Reponse
                        {
                            Contenu = "Pour améliorer le sens de l'orientation",
                            EstCorrecte = true,
                        },
                        new Reponse
                        {
                            Contenu = "Pour savoir mesurer précisément au mètre près",
                            EstCorrecte = false,
                        },
                        new Reponse
                        {
                            Contenu = "Pour calculer le temps de trajet exact",
                            EstCorrecte = false,
                        },
                    },
                    IdReponseCorrecte = 1,
                };

                var question2 = new Question
                {
                    Contenu = "Quelle marge de précision est-elle pertinente de savoir estimer ?",
                    Reponses = new List<Reponse>
                    {
                        new Reponse { Contenu = "+/- 1 mètre", EstCorrecte = false },
                        new Reponse { Contenu = "+/- 10 mètres", EstCorrecte = true },
                        new Reponse { Contenu = "+/- 1000 mètres", EstCorrecte = false },
                    },
                    IdReponseCorrecte = 2,
                };

                var question3 = new Question
                {
                    Contenu = "Qu'est-ce qu'une bonne estimation des distances permet de faire ?",
                    Reponses = new List<Reponse>
                    {
                        new Reponse { Contenu = "Choisir le meilleur chemin", EstCorrecte = false },
                        new Reponse
                        {
                            Contenu = "Éviter de se perdre en estimant les intersections",
                            EstCorrecte = true,
                        },
                        new Reponse
                        {
                            Contenu = "Faire des calculs complexes de distance",
                            EstCorrecte = false,
                        },
                    },
                    IdReponseCorrecte = 2,
                };

                context.Questions.AddRange(question1, question2, question3);
            }

            // Ajouter les astuces si ce n'est pas déjà fait
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
