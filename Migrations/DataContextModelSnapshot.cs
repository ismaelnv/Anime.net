﻿using AnimeWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AnimeWeb.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {

            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("AnimeModelGenreModel", b =>
                {
                    b.Property<int>("Genresid")
                        .HasColumnType("INTEGER");

                    b.Property<int>("animesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Genresid", "animesId");

                    b.HasIndex("animesId");

                    b.ToTable("AnimeModelGenreModel");
                });

            modelBuilder.Entity("AnimeWeb.Models.AnimeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("state")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("updateDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("uploadDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Anime");
                });

            modelBuilder.Entity("AnimeWeb.Models.ChapterModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("animeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("episode")
                        .HasColumnType("INTEGER");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("state")
                        .HasColumnType("INTEGER");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updateDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("uploadDate")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("animeId");

                    b.ToTable("Capitulo");
                });

            modelBuilder.Entity("AnimeWeb.Models.GenreModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("state")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("updateDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("uploadDate")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("AnimeWeb.Models.StudioModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("state")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("updateDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("uploadDate")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Studio");
                });

            modelBuilder.Entity("AnimeWeb.Models.VideoModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("idChapter")
                        .HasColumnType("INTEGER");

                    b.Property<string>("language")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("state")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("updateDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("uploadDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("idChapter");

                    b.ToTable("Video");
                });

            modelBuilder.Entity("ImageModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("animeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("chapterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("imageCategory")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("imageType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("state")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("updateDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("uploadDate")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("animeId");

                    b.HasIndex("chapterId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Studio_Anime", b =>
                {
                    b.Property<int>("AnimesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Studiosid")
                        .HasColumnType("INTEGER");

                    b.HasKey("AnimesId", "Studiosid");

                    b.HasIndex("Studiosid");

                    b.ToTable("Studio_Anime");
                });

            modelBuilder.Entity("AnimeModelGenreModel", b =>
                {
                    b.HasOne("AnimeWeb.Models.GenreModel", null)
                        .WithMany()
                        .HasForeignKey("Genresid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeWeb.Models.AnimeModel", null)
                        .WithMany()
                        .HasForeignKey("animesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeWeb.Models.ChapterModel", b =>
                {
                    b.HasOne("AnimeWeb.Models.AnimeModel", "AnimeModel")
                        .WithMany("Chapters")
                        .HasForeignKey("animeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimeModel");
                });

            modelBuilder.Entity("AnimeWeb.Models.VideoModel", b =>
                {
                    b.HasOne("AnimeWeb.Models.ChapterModel", "ChapterModel")
                        .WithMany("videos")
                        .HasForeignKey("idChapter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChapterModel");
                });

            modelBuilder.Entity("ImageModel", b =>
                {
                    b.HasOne("AnimeWeb.Models.AnimeModel", null)
                        .WithMany("Images")
                        .HasForeignKey("animeId");

                    b.HasOne("AnimeWeb.Models.ChapterModel", null)
                        .WithMany("Images")
                        .HasForeignKey("chapterId");
                });

            modelBuilder.Entity("Studio_Anime", b =>
                {
                    b.HasOne("AnimeWeb.Models.AnimeModel", null)
                        .WithMany()
                        .HasForeignKey("AnimesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeWeb.Models.StudioModel", null)
                        .WithMany()
                        .HasForeignKey("Studiosid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeWeb.Models.AnimeModel", b =>
                {
                    b.Navigation("Chapters");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("AnimeWeb.Models.ChapterModel", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("videos");
                });
#pragma warning restore 612, 618
        }
    }
}
