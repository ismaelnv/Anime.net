
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
        public DbSet<CapituloModel> Capitulo { get; set; }
    }
}