using AnimeWeb.Data;

namespace AnimeWeb.Repository.IRepository
{

 public class ImageRepository : Repository<ImageModel>, IImageRepository
    {

        private readonly DataContext _db;

        public ImageRepository(DataContext db) : base(db)
        {

            _db = db;
        }

        // public async Task<StudioModel> GetStudioAndAnimes(int id)
        // {
            
        //     StudioModel studio = await _db.Studio.Where(s => s.id == id).Include(s => s.Animes).FirstAsync();
        //     return studio;
        // }

        // public async Task<List<StudioModel>> GetByIdsAsync(List<int> studioIds)
        // {
            
        //     return await _db.Studio.Where(s => studioIds.Contains(s.id)).ToListAsync();
        // }

        public async Task<ImageModel> UpdateAsync(ImageModel image)
        {

            image.updateDate = DateTime.Now;
            _db.Update(image);
            
            await _db.SaveChangesAsync();
            return image;
        }
    }   
}