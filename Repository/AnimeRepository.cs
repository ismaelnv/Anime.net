using AnimeWeb.Data;
using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AnimeWeb.Repository
{
    public class AnimeRepository : Repository<AnimeModel>, IAnimeRepository
    {

        private readonly DataContext _db;

        public AnimeRepository(DataContext db) : base(db)
        {

            _db = db;
        }

        public async Task<List<AnimeModel>> GetAnimes()
        {

            List<AnimeModel> animes = await _db.Anime.Include( A => A.Images).ToListAsync();

           foreach(var anime in animes)
            {

                List<GenreModel> genres = await _db.Genre.Where(g => g.animes.Any(ag => ag.Id == anime.Id)).ToListAsync();
                anime.Genres= genres;
            }

            return animes;
        }

        public async Task<AnimeModel> GetanimeChaptersAsync(int id)
        {

            AnimeModel anime = await _db.Anime.Where(A => A.Id == id).Include(A => A.Chapters).ThenInclude(c => c.Images).FirstAsync();
            return anime;
        }

        public async Task<AnimeModel> UpdateAsync(AnimeModel entidad)
        {

            entidad.updateDate = DateTime.Now;
            _db.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }

        public async Task<List<AnimeDto>> GetAllAnimeDtoAsync()
        {

            var animeDto = await _db.Anime
            .Select(anime => new AnimeDto
            {
                Id = anime.Id,
                name = anime.name,
                state = anime.state
                
            })
            
            .ToListAsync();
            return animeDto;
        }

        public async Task<AnimeModel> GetAnimeAndGenres(int id)
        {
            
            AnimeModel anime = await _db.Anime.Where(A => A.Id == id).Include(A => A.Genres).FirstAsync();
            return anime;           
        }

        public async Task<AnimeModel> GetAnimeAndStudios(int id)
        {

            AnimeModel anime = await _db.Anime.Where(A => A.Id == id).Include(A => A.Studios).FirstAsync();
            return anime;
        }

        public async Task<AnimeModel> GetAnimeAndImages(int id)
        {

            AnimeModel anime = await _db.Anime.Where(A => A.Id == id).Include(A => A.Images).FirstAsync();
            return anime;
        }

        public async Task<AnimeModel> GetAnimeByIdWithAttributs(int id)
        {

            return await _db.Anime.Where( A => A.Id == id).Include(A => A.Images).Include( A => A.Genres).FirstAsync();
        }

        public async Task<List<AnimeModel>>GetAnimesByName(string animeName)
        {

            List<AnimeModel> animes = new List<AnimeModel>();

            using (_db) 
            {

                var allAnimes = await _db.Anime.Include(a => a.Images).ToListAsync();

                animes = allAnimes
                .Where(a => a.name.Contains(animeName,StringComparison.OrdinalIgnoreCase))
                .ToList();

                foreach(var anime in animes)
                {

                    List<GenreModel> genres = await _db.Genre.Where(g => g.animes.Any(ag => ag.Id == anime.Id)).ToListAsync();
                    anime.Genres= genres;
                }
            }

            return animes;
        }
    }
}