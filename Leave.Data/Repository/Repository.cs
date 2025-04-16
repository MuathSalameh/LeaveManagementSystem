using Leave.Data.Repository.IRepository;
using Leave.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Data.Repository
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
        public IEnumerable<T> GetAll(string? properties = null)
        {
            IQueryable<T> queryable = _dbset;
            if(!string.IsNullOrEmpty(properties))
            {
                foreach (var prop in properties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(prop);
                }
            }
            return queryable.ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> filter , string? properties=null)
        {
            IQueryable<T> query = _dbset;
            query= query.Where(filter);
            if (!string.IsNullOrEmpty(properties))
            {
                foreach (var prop in properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
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
