using System.Linq.Expressions;

namespace StokTakip.Core.Interfaces
{
    // <T> demek: Hangi tabloyu (Product, Company) gönderirsem onun için çalış demek.
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        // Özel sorgu yazabilmek için (Fiyatı 50'den büyük olanlar vs.)
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}