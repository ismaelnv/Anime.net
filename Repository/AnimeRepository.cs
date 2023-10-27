
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

        public async Task<AnimeModel> getanimeChaptersAsync(int id)
        {

            AnimeModel anime = await _db.Anime.Where(A => A.Id == id).Include(A => A.chapters).FirstAsync();
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
    }
}