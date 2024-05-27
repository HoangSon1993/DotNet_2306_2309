using Eshop.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Utils
{
    public class Repository<T>:IRepository<T> where T : class
    {
        private readonly EshopContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(EshopContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
