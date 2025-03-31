﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiNORDev.Migrations
{
    [DbContext(typeof(ApiNORDevContext))]
    partial class ApiNORDevContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("ApiNORDev.Model.Astuce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Contenu")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Astuces");
                });

            modelBuilder.Entity("ApiNORDev.Model.Competence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Lien")
                        .HasColumnType("TEXT");

                    b.Property<string>("Titre")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Competences");
                });

            modelBuilder.Entity("ApiNORDev.Model.Exercice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DistanceCible")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nom")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Exercices");
                });

            modelBuilder.Entity("ApiNORDev.Model.Lecon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Objectif")
                        .HasColumnType("TEXT");

                    b.Property<string>("Titre")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Lecons");
                });

            modelBuilder.Entity("ApiNORDev.Model.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EstCorrecte")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionQuizId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Texte")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionQuizId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("ApiNORDev.Model.QuestionQuiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Explication")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("QuizId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("QuestionsQuiz");
                });

            modelBuilder.Entity("ApiNORDev.Model.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("ApiNORDev.Model.Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("ApiNORDev.Model.Option", b =>
                {
                    b.HasOne("ApiNORDev.Model.QuestionQuiz", "QuestionQuiz")
                        .WithMany("Options")
                        .HasForeignKey("QuestionQuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionQuiz");
                });

            modelBuilder.Entity("ApiNORDev.Model.QuestionQuiz", b =>
                {
                    b.HasOne("ApiNORDev.Model.Quiz", "Quiz")
                        .WithMany("QuestionsQuiz")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("ApiNORDev.Model.QuestionQuiz", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("ApiNORDev.Model.Quiz", b =>
                {
                    b.Navigation("QuestionsQuiz");
                });
#pragma warning restore 612, 618
        }
    }
}
