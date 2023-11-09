using AnimeWeb.Data;
using AnimeWeb.Models;
using AnimeWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AnimeWeb.Repository
{
    public class GenreRepository : Repository<GenreModel>, IGenreRepository
    {
        private readonly DataContext _db;

        public GenreRepository(DataContext db) : base(db)
        {

            _db = db;
        }

        public async Task<GenreModel> getGenreAnimesAsync(int id)
        {

            GenreModel genre = await _db.Genre.Where(G => G.id == id).Include(G => G.animes).FirstAsync();
            return genre;
        }

        public async Task<GenreModel> UpdateAsync(GenreModel entidad)
        {
            
            entidad.updateDate = DateTime.Now;
            _db.Update(entidad);
            
            await _db.SaveChangesAsync();
            return entidad;
        }

        public async Task<List<AnimeModel>> GetAnimesByGenreAsync(string nameGenre)
        {

            List<AnimeModel> animes = new List<AnimeModel>();

            using (_db) 
            {

                animes = await _db.Anime
                .Where(a => a.Genres.Any(g => g.name == nameGenre))
                .ToListAsync();
            }

            return animes;
        }
    }
}