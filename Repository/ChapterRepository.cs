using AnimeWeb.Data;
using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;

namespace AnimeWeb.Repository
{
    public class ChapterRepository : Repository<ChapterModel>, IChapterRepository
    {
        private readonly DataContext _db;

        public ChapterRepository(DataContext db) : base(db)
        {
            _db = db;
        }
        
        public async Task<ChapterModel> Update(ChapterModel entidad)
        {
            entidad.updateDate = DateTime.Now;
            _db.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}