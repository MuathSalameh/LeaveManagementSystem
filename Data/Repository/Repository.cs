using Data.Repository.IRepository;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbset;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }
         public IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbset;
            query= query.Where(filter);

            return query.FirstOrDefault();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void Remove(T entity)
        {
            _dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
           _dbset.RemoveRange(entities);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
