using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using WebsitSellsLaptop.Data;
using WebsitSellsLaptop.Repository.IRepository;

namespace WebsitSellsLaptop.Repository
{
    public class Repository<T>  : IRepository<T> where T : class

    {
        private readonly ApplicationDbContext dbContext;
        private DbSet<T> dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }
        public IEnumerable<T> GetWithIncludes(
           Func<IQueryable<T>, IQueryable<T>> includeFunc,
           Expression<Func<T, bool>>? expression = null,
           bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            query = includeFunc(query);

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return query.ToList();
        }
        public void Create(T entity)
        {
            try
            {
                dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating entity", ex);
            }
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Edit(T entity)
        {
            dbSet.Update(entity);
        }

        public IEnumerable<T> Get(Expression<Func<T, object>>[]? includeProps = null, Expression<Func<T, bool>>? expression = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includeProps != null)
            {
                foreach (var prop in includeProps)
                {
                    query = query.Include(prop);
                }
            }

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return query.ToList();
        }


        public T? GetOne(Expression<Func<T, object>>[]? includeProps = null, Expression<Func<T, bool>>? expression = null, bool tracked = true)
        {
            return Get(includeProps, expression, tracked).FirstOrDefault();
        }

    }

}
