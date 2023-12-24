using AnimeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeWeb.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<AnimeModel> Anime { get; set; }
        public DbSet<ChapterModel> Capitulo { get; set; }
        public DbSet<VideoModel> Video { get; set; }
        public DbSet<GenreModel> Genre { get; set; }
        public DbSet<StudioModel> Studio { get; set; }
        public DbSet<ImageModel> Image { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //Relacion de muchos a uno de los modelos AnimeModel y ChapterModel
            modelBuilder.Entity<AnimeModel>()
            .HasMany(a => a.Chapters)
            .WithOne(e => e.AnimeModel)
            .HasForeignKey(e => e.animeId)
            .IsRequired();
            
            //Relacion de muchos a uno de los modelos ChapterModel y VideoModel
            modelBuilder.Entity<ChapterModel>()
            .HasMany(c => c.videos)
            .WithOne(e => e.ChapterModel)
            .HasForeignKey(e => e.idChapter)
            .IsRequired();

            //Relacion de muchos a muchos de las tablas Genre y Animes
            modelBuilder.Entity<GenreModel>()
            .HasMany(g => g.animes)
            .WithMany(e => e.Genres);

            
            //Relacion de muchos a muchos de las tablas Studio y Anime
            modelBuilder.Entity<StudioModel>()
            .HasMany(s => s.Animes)
            .WithMany(e => e.Studios)
            .UsingEntity("Studio_Anime");  

            //Relacion de uno  a muchos de los modelos ImageModel Y AnimeModel
            modelBuilder.Entity<AnimeModel>()
            .HasMany(e => e.Images)
            .WithOne()
            .HasForeignKey(e => e.animeId)
            .IsRequired(false);

            //Relacion de uno a muchos chapterModel Y ImageModel
            modelBuilder.Entity<ChapterModel>()
            .HasMany(e => e.Images)
            .WithOne()
            .HasForeignKey(e => e.chapterId)
            .IsRequired(false);

            //Para que los enum se creen como cadenas
            modelBuilder.Entity<ImageModel>()
            .Property(e => e.imageType)
            .HasConversion<string>();
            
            modelBuilder.Entity<ImageModel>()
            .Property(e => e.imageCategory)
            .HasConversion<string>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.EnableSensitiveDataLogging(true); // Habilita el registro detallado
        }
    }
}