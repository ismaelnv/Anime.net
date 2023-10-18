using System.Linq.Expressions;

namespace AnimeWeb.Repository.IRepository
{
    public interface IRespository<T> where T : class
    {
        Task Create(T entidad);

        Task<List<T>> GetAll(Expression<Func<T,bool>>? filtro = null);

        Task<T> Obtain(Expression<Func<T,bool>> filtro = null, bool tracked = true);

        Task Remove(T entidad);

        Task Engrave();
        
    }
}