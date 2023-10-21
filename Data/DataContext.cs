
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimeModel>()
            .HasMany(a => a.chapters)
            .WithOne(e => e.AnimeModel)
            .HasForeignKey(e => e.animeId)
            .IsRequired();

            //modelBuilder.Entity<CapituloModel>()
            //.HasOne(c => c.AnimeModel)
            //.WithMany(e => e.chapters)
            //.HasForeignKey(e => e.animeId)
            //.IsRequired();
        }
    }
}