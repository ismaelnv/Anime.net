using System.Linq.Expressions;
using AnimeWeb.Data;
using AnimeWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AnimeWeb.Repository
{
    public class Repository<T> : IRespository<T> where T : class
    {
        private readonly DataContext _db;
        internal DbSet<T> dbSet;

        public Repository(DataContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task CreateAsync(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await EngraveAsync();
        }

        public async Task EngraveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> ObtainAsync(Expression<Func<T, bool>> filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            
            if(!tracked){
                query = query.AsNoTracking();
            }

            if(filtro != null){
                query = query.Where(filtro);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbSet;
            
            if(filtro != null){
                query = query.Where(filtro);
            }

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entidad)
        {
           dbSet.Remove(entidad);
           await EngraveAsync();
        }
    }
}