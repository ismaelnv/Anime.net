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
                .Where(a => a.Genres.Any(g => g.name == nameGenre)).Include(A => A.Images)
                .ToListAsync();

                foreach (var anime in animes)
                {
                    // Obtiene los animes para este género específico.
                    var animesForGenre = await _db.Genre
                        .Where(genre => genre.animes.Any(a => a.Id == anime.Id))
                        //.Include(anime => anime.Genres) // Incluye los géneros para cada anime.
                        .ToListAsync();

                    // Asigna los animes al género.
                    anime.Genres = animesForGenre;
                }
           }
            
            return animes;
        }

        public async Task<List<GenreModel>> GetByIdsAsync(List<int> genreIds)
        {
            
            return await _db.Genre.Where(s => genreIds.Contains(s.id)).ToListAsync();
        }

        //Prueba para resolver el error de generos
        public async Task<List<GenreModel>> GetGenres()
        {
            List<GenreModel> genres = await _db.Genre.ToListAsync();

            // Para cada género, carga los animes asociados.
            foreach (var genre in genres)
            {
                // Obtiene los animes para este género específico.
                var animesForGenre = await _db.Anime
                    .Where(anime => anime.Genres.Any(g => g.id == genre.id))
                    //.Include(anime => anime.Genres) // Incluye los géneros para cada anime.
                    .ToListAsync();

                // Asigna los animes al género.
                genre.animes = animesForGenre;
            }  
            return genres;
        }
    }
}