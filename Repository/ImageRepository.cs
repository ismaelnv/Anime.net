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

        public async Task<ImageModel> UpdateAsync(ImageModel image)
        {

            image.updateDate = DateTime.Now;
            _db.Update(image);
            
            await _db.SaveChangesAsync();
            return image;
        }
    }   
}