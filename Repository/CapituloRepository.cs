using AnimeWeb.Data;
using AnimeWeb.Models;
using AnimeWeb.Repository.IRepository;

namespace AnimeWeb.Repository
{
    public class CapituloRepository : Repository<CapituloModel>, ICapituloRepository
    {
        private readonly DataContext _db;

        public CapituloRepository(DataContext db) : base(db)
        {
            _db = db;

        }
        public async Task<CapituloModel> Update(CapituloModel entidad)
        {
            entidad.updateDate = DateTime.Now;
            _db.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}