namespace AnimeWeb.Repository.IRepository
{
    public interface IImageRepository : IRespository <ImageModel>
    {
        
        Task<ImageModel> UpdateAsync(ImageModel image);  
    }
}