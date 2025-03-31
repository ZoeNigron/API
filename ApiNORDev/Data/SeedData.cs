using ApiNORDev.Model;

namespace ApiNORDev.Data
{
    public static class SeedData
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApiNORDevContext>();

            if (
                context.Utilisateurs.Any()
                && context.Astuces.Any()
                && context.Competences.Any()
                && context.Exercices.Any()
                && context.Lecons.Any()
                && context.QuestionsQuiz.Any()
                && context.Options.Any()
                && context.Quizzes.Any()
            )
            {
                return;
            }

            var utilisateur1 = new Utilisateur
            {
                Nom = "Nigron",
                Prenom = "Zoé",
                Email = "znigron@ensc.fr",
                MotDePasse = "1234",
            };

            context.Utilisateurs.Add(utilisateur1);

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

            var exercices = new[]
            {
                new Exercice { Nom = "Exercice 20m", DistanceCible = 20 },
                new Exercice { Nom = "Exercice 50m", DistanceCible = 50 },
                new Exercice { Nom = "Exercice 100m", DistanceCible = 100 },
            };

            context.Exercices.AddRange(exercices);

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

            var quiz1 = new Quiz { Titre = "Apprendre à évaluer les distances" };

            context.Quizzes.Add(quiz1);
            context.SaveChanges();

            var question1 = new QuestionQuiz
            {
                Question = "Pourquoi est-il important d'apprendre à évaluer les distances ?",
                Explication =
                    "Apprendre à évaluer les distances améliore la capacité à se repérer dans l'espace, ce qui est essentiel pour l'orientation.",
                QuizId = quiz1.Id,
            };

            var question2 = new QuestionQuiz
            {
                Question =
                    "Quelle marge de précision est-elle pertinente pour estimer une distance ?",
                Explication =
                    "Une estimation précise à +/- 10 mètres est généralement suffisante pour la plupart des activités de navigation.",
                QuizId = quiz1.Id,
            };

            var question3 = new QuestionQuiz
            {
                Question = "Qu'est-ce qu'une bonne estimation des distances permet de faire ?",
                Explication =
                    "Une bonne estimation des distances permet de mieux s'orienter et d'éviter de se perdre en estimant les intersections.",
                QuizId = quiz1.Id,
            };

            context.QuestionsQuiz.AddRange(question1, question2, question3);
            context.SaveChanges();

            Console.WriteLine($"Question 1 ID: {question1.Id}");
            Console.WriteLine($"Question 2 ID: {question2.Id}");
            Console.WriteLine($"Question 3 ID: {question3.Id}");

            if (question1.Id == 0 || question2.Id == 0 || question3.Id == 0)
            {
                throw new Exception("Les IDs des QuestionQuiz ne sont pas générés !");
            }

            var option1 = new Option
            {
                QuestionQuizId = question1.Id,
                Texte = "Pour améliorer le sens de l'orientation",
                EstCorrecte = true,
            };
            var option2 = new Option
            {
                QuestionQuizId = question1.Id,
                Texte = "Pour savoir mesurer précisément au mètre près",
                EstCorrecte = false,
            };
            var option3 = new Option
            {
                QuestionQuizId = question1.Id,
                Texte = "Pour calculer le temps de trajet exact",
                EstCorrecte = false,
            };
            var option4 = new Option
            {
                QuestionQuizId = question2.Id,
                Texte = "+/- 1 mètre",
                EstCorrecte = false,
            };
            var option5 = new Option
            {
                QuestionQuizId = question2.Id,
                Texte = "+/- 10 mètres",
                EstCorrecte = true,
            };
            var option6 = new Option
            {
                QuestionQuizId = question2.Id,
                Texte = "+/- 1000 mètres",
                EstCorrecte = true,
            };
            var option7 = new Option
            {
                QuestionQuizId = question3.Id,
                Texte = "Choisir le meilleur chemin",
                EstCorrecte = true,
            };
            var option8 = new Option
            {
                QuestionQuizId = question3.Id,
                Texte = "Éviter de se perdre en estimant les intersections",
                EstCorrecte = false,
            };
            var option9 = new Option
            {
                QuestionQuizId = question3.Id,
                Texte = "Faire des calculs complexes de distance",
                EstCorrecte = false,
            };

            context.Options.AddRange(
                option1,
                option2,
                option3,
                option4,
                option5,
                option6,
                option7,
                option8,
                option9
            );
            context.SaveChanges();

            var quiz2 = new Quiz { Titre = "S'orienter avec les points cardinaux" };

            context.Quizzes.Add(quiz2);
            context.SaveChanges();

            var question4 = new QuestionQuiz
            {
                Question = "Quels sont les quatre points cardinaux principaux ?",
                Explication =
                    "Les quatre points cardinaux principaux sont le Nord, le Sud, l'Est et l'Ouest.",
                QuizId = quiz2.Id,
            };

            var question5 = new QuestionQuiz
            {
                Question = "Comment peut-on déterminer le Nord en pleine nature ?",
                Explication =
                    "Il existe plusieurs méthodes, comme utiliser une boussole ou observer l'ombre d'un objet à midi.",
                QuizId = quiz2.Id,
            };

            var question6 = new QuestionQuiz
            {
                Question = "Pourquoi les points cardinaux sont-ils importants pour s'orienter ?",
                Explication =
                    "Les points cardinaux permettent de s'orienter dans l'espace, surtout en plein air ou dans des environnements inconnus.",
                QuizId = quiz2.Id,
            };

            context.QuestionsQuiz.AddRange(question4, question5, question6);
            context.SaveChanges();

            if (question4.Id == 0 || question5.Id == 0 || question6.Id == 0)
            {
                throw new Exception("Les IDs des QuestionQuiz ne sont pas générés !");
            }

            var option10 = new Option
            {
                QuestionQuizId = question4.Id,
                Texte = "Nord, Sud, Est, Ouest",
                EstCorrecte = true,
            };
            var option11 = new Option
            {
                QuestionQuizId = question4.Id,
                Texte = "Nord, Est, Ouest, Sud",
                EstCorrecte = true,
            };
            var option12 = new Option
            {
                QuestionQuizId = question4.Id,
                Texte = "Nord, Est, Ouest, Sud-Est",
                EstCorrecte = false,
            };

            var option13 = new Option
            {
                QuestionQuizId = question5.Id,
                Texte = "En observant les étoiles",
                EstCorrecte = true,
            };
            var option14 = new Option
            {
                QuestionQuizId = question5.Id,
                Texte = "En utilisant un GPS",
                EstCorrecte = false,
            };
            var option15 = new Option
            {
                QuestionQuizId = question5.Id,
                Texte = "En suivant les routes",
                EstCorrecte = false,
            };

            var option16 = new Option
            {
                QuestionQuizId = question6.Id,
                Texte = "Pour mieux se repérer dans l'espace",
                EstCorrecte = true,
            };
            var option17 = new Option
            {
                QuestionQuizId = question6.Id,
                Texte = "Pour suivre une ligne droite",
                EstCorrecte = false,
            };
            var option18 = new Option
            {
                QuestionQuizId = question6.Id,
                Texte = "Pour savoir où se trouvent les villes",
                EstCorrecte = false,
            };

            context.Options.AddRange(
                option10,
                option11,
                option12,
                option13,
                option14,
                option15,
                option16,
                option17,
                option18
            );
            context.SaveChanges();

            var quiz3 = new Quiz { Titre = "Distance et points cardinaux" };

            context.Quizzes.Add(quiz3);
            context.SaveChanges();

            var question7 = new QuestionQuiz
            {
                Question = "Comment utiliser les points cardinaux pour évaluer la distance ?",
                Explication =
                    "Les points cardinaux aident à comprendre la direction dans laquelle on se déplace, ce qui peut être utile pour évaluer les distances.",
                QuizId = quiz3.Id,
            };

            var question8 = new QuestionQuiz
            {
                Question =
                    "Quelle méthode utiliser pour estimer la distance entre deux points en utilisant les points cardinaux ?",
                Explication =
                    "On peut estimer la distance en suivant les directions cardinaux et en observant les repères naturels sur le terrain.",
                QuizId = quiz3.Id,
            };

            var question9 = new QuestionQuiz
            {
                Question =
                    "Pourquoi est-il utile de connaître la direction des points cardinaux lors d'un déplacement sur une longue distance ?",
                Explication =
                    "Connaître la direction permet de s'assurer qu'on se déplace dans la bonne direction et d'éviter de se perdre.",
                QuizId = quiz3.Id,
            };

            context.QuestionsQuiz.AddRange(question7, question8, question9);
            context.SaveChanges();

            if (question7.Id == 0 || question8.Id == 0 || question9.Id == 0)
            {
                throw new Exception("Les IDs des QuestionQuiz ne sont pas générés !");
            }

            var option19 = new Option
            {
                QuestionQuizId = question7.Id,
                Texte = "En suivant une carte et les directions",
                EstCorrecte = true,
            };
            var option20 = new Option
            {
                QuestionQuizId = question7.Id,
                Texte = "En estimant la durée du trajet",
                EstCorrecte = false,
            };
            var option21 = new Option
            {
                QuestionQuizId = question7.Id,
                Texte = "En calculant les angles de vision",
                EstCorrecte = false,
            };

            var option22 = new Option
            {
                QuestionQuizId = question8.Id,
                Texte = "En suivant les lignes droites entre deux points",
                EstCorrecte = false,
            };
            var option23 = new Option
            {
                QuestionQuizId = question8.Id,
                Texte = "En observant les points de repère dans chaque direction cardinale",
                EstCorrecte = true,
            };
            var option24 = new Option
            {
                QuestionQuizId = question8.Id,
                Texte = "En utilisant un calcul mathématique complexe",
                EstCorrecte = false,
            };

            var option25 = new Option
            {
                QuestionQuizId = question9.Id,
                Texte = "Cela permet de mieux gérer l'orientation et de savoir où l'on va",
                EstCorrecte = true,
            };
            var option26 = new Option
            {
                QuestionQuizId = question9.Id,
                Texte = "Cela permet de calculer la vitesse du déplacement",
                EstCorrecte = false,
            };
            var option27 = new Option
            {
                QuestionQuizId = question9.Id,
                Texte = "Cela permet d'estimer la hauteur d'un terrain",
                EstCorrecte = false,
            };

            context.Options.AddRange(
                option19,
                option20,
                option21,
                option22,
                option23,
                option24,
                option25,
                option26,
                option27
            );
            context.SaveChanges();
        }
    }
}
