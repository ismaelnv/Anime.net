
using AnimeWeb.Data;
using AnimeWeb.Models;
using AnimeWeb.Repository.IRepository;

namespace AnimeWeb.Repository
{
    public class AnimeRepository : Repository<AnimeModel>, IAnimeRepository
    {
        private readonly DataContext _db;

        public AnimeRepository(DataContext db) : base(db)
        {
            _db = db;
        }

        public async Task<AnimeModel> Update(AnimeModel entidad)
        {
            entidad.updateDate = DateTime.Now;
            _db.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}