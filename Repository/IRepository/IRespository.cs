using System.Linq.Expressions;

namespace AnimeWeb.Repository.IRepository
{
    public interface IRespository<T> where T : class
    {
        Task CreateAsync(T entidad);

        Task<List<T>> GetAllAsync(Expression<Func<T,bool>>? filtro = null);

        Task<T> ObtainAsync(Expression<Func<T,bool>> filtro = null, bool tracked = true);

        Task RemoveAsync(T entidad);

        Task EngraveAsync(); 
    }
}