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
            if (!context.Competences.Any())
            {
                var competences = new[]
                {
                    new Competence
                    {
                        Titre = "Compétence 1",
                        Description = "Évaluer les distances",
                        Lien = "/evaluer-les-distances",
                    },
                    new Competence
                    {
                        Titre = "Compétence 2",
                        Description = "S'orienter avec les points cardinaux",
                        Lien = "/page-non-developpee",
                    },
                    new Competence
                    {
                        Titre = "Compétence 3",
                        Description = "Distances et points cardinaux",
                        Lien = "/page-non-developpee",
                    },
                };

                context.Competences.AddRange(competences);
            }

            if (!context.Exercices.Any())
            {
                var exercices = new[]
                {
                    new Exercice { Nom = "Exercice 20m", DistanceCible = 20 },
                    new Exercice { Nom = "Exercice 50m", DistanceCible = 50 },
                    new Exercice { Nom = "Exercice 100m", DistanceCible = 100 },
                };

                context.Exercices.AddRange(exercices);
            }
            if (!context.Lecons.Any())
            {
                var lecons = new[]
                {
                    new Lecon
                    {
                        Titre = "Leçon 1 : Estimation des distances en se déplaçant",
                        Description = "Estimer une distance après avoir marché.",
                        Objectif =
                            "Appuyez sur le bouton 'Démarrer' puis déplacez-vous du nombre de mètres nécessaire. Quand vous estimez être arrivé, appuyez sur le bouton 'Terminer'.",
                    },
                    new Lecon
                    {
                        Titre = "Leçon 2 : Estimation des distances sans se déplacer",
                        Description = "Estimer une distance en regardant devant soi.",
                        Objectif =
                            "Regardez devant vous et ciblez un point fixe. Cliquez sur la carte afin de positionner ce point, puis estimez la distance à laquelle vous vous trouvez de ce point.",
                    },
                };

                context.Lecons.AddRange(lecons);
            }

            if (!context.QuestionsQuiz.Any())
            {
                var questionsQuiz = new[]
                {
                    new QuestionQuiz
                    {
                        Question =
                            "Pourquoi est-il important d'apprendre à évaluer les distances ?",
                        Options = new List<Option>
                        {
                            new Option
                            {
                                Texte = "Pour améliorer le sens de l'orientation",
                                EstCorrecte = true,
                            },
                            new Option
                            {
                                Texte = "Pour savoir mesurer précisément au mètre près",
                                EstCorrecte = false,
                            },
                            new Option
                            {
                                Texte = "Pour calculer le temps de trajet exact",
                                EstCorrecte = false,
                            },
                        },
                        Explication =
                            "Apprendre à évaluer les distances améliore la capacité à se repérer dans l'espace.",
                    },
                    new QuestionQuiz
                    {
                        Question =
                            "Quelle marge de précision est-elle pertinente de savoir estimer ?",
                        Options = new List<Option>
                        {
                            new Option { Texte = "+/- 1 mètre", EstCorrecte = false },
                            new Option { Texte = "+/- 10 mètres", EstCorrecte = true },
                            new Option { Texte = "+/- 1000 mètres", EstCorrecte = false },
                        },
                        Explication =
                            "Une estimation précise à +/- 10 mètres est généralement suffisante.",
                    },
                };

                context.QuestionsQuiz.AddRange(questionsQuiz);
                context.SaveChanges();
            }

            context.SaveChanges();
        }
    }
}
