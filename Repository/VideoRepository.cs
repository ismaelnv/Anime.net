using AnimeWeb.Data;
using AnimeWeb.Models;
using AnimeWeb.Repository.IRepository;
namespace AnimeWeb.Repository
{
    public class VideoRepository : Repository<VideoModel>, IVideoRepository
    {

        private readonly DataContext _db;

        public VideoRepository(DataContext db) : base(db)
        { 
            _db = db;
        }

        public async Task<VideoModel> UpdateAsync(VideoModel entidad)
        {
            entidad.updateDate = DateTime.Now;
            _db.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}