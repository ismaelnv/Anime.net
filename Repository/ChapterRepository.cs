using AnimeWeb.Data;
using AnimeWeb.Models;
using AnimeWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AnimeWeb.Repository
{
    public class ChapterRepository : Repository<ChapterModel>, IChapterRepository
    {

        private readonly DataContext _db;

        public ChapterRepository(DataContext db) : base(db)
        {

            _db = db;
        }

        public async Task<List<ChapterModel>> GetChapters()
        {

            List<ChapterModel> chapters = await _db.Capitulo.Include(c => c.Images).ToListAsync();
            return chapters;
        }

        public async Task<ChapterModel> getChapterVideosAsync(int id)
        {

           ChapterModel chapter = await _db.Capitulo.Where(c => c.id == id).Include(c => c.videos).FirstAsync();
           return chapter;
        }

        public async Task<ChapterModel> UpdateAsync(ChapterModel entidad)
        {

            entidad.updateDate = DateTime.Now;
            _db.Update(entidad);
            
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}