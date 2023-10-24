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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relacion de muchos a uno de los modelos AnimeModel y ChapterModel
            modelBuilder.Entity<AnimeModel>()
            .HasMany(a => a.chapters)
            .WithOne(e => e.AnimeModel)
            .HasForeignKey(e => e.animeId)
            .IsRequired();
            
            //Relacion de muchos a uno de los modelos ChapterModel y VideoModel
            modelBuilder.Entity<ChapterModel>()
            .HasMany(c => c.videos)
            .WithOne(e => e.ChapterModel)
            .HasForeignKey(e => e.idChapter)
            .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.EnableSensitiveDataLogging(true); // Habilita el registro detallado
        }
    }
}