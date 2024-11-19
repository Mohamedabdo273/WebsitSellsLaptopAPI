using System.Linq.Expressions;

namespace WebsitSellsLaptop.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
        // CRUD
        public IEnumerable<T> Get(Expression<Func<T, object>>[]? includeProps = null, Expression<Func<T, bool>>? expression = null, bool tracked = true);

        T? GetOne(Expression<Func<T, object>>[]? includeProps = null, Expression<Func<T, bool>>? expression = null, bool tracked = true);

        void Create(T entity);
        public IEnumerable<T> GetWithIncludes(
           Func<IQueryable<T>, IQueryable<T>> includeFunc,
           Expression<Func<T, bool>>? expression = null,
           bool tracked = true);

        void Edit(T entity);

        void Delete(T entity);

        void Commit();


    }
}
