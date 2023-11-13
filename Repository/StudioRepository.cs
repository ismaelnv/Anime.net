using AnimeWeb.Data;
using AnimeWeb.Models;
using AnimeWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AnimeWeb.Repository
{
    public class StudioRepository : Repository<StudioModel>, IStudioRepository
    {

        private readonly DataContext _db;

        public StudioRepository(DataContext db) : base(db)
        {

            _db = db;
        }

        public async Task<StudioModel> GetStudioAndAnimes(int id)
        {
            
            StudioModel studio = await _db.Studio.Where(s => s.id == id).Include(s => s.Animes).FirstAsync();
            return studio;
        }

        public async Task<List<StudioModel>> GetByIdsAsync(List<int> studioIds)
        {
            
            return await _db.Studio.Where(s => studioIds.Contains(s.id)).ToListAsync();
        }

        public async Task<StudioModel> UpdateAsync(StudioModel entidad)
        {

            entidad.updateDate = DateTime.Now;
            _db.Update(entidad);
            
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}