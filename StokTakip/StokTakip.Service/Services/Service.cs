using Microsoft.EntityFrameworkCore;
using StokTakip.Core.Interfaces;
using StokTakip.Repository; // AppDbContext için gerekli
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StokTakip.Service.Services
{
    // Generic Service implementation
    public class Service<T> : IService<T> where T : class
    {
        // 1. Repository'i çağırıyoruz (Veri işlemleri için)
        private readonly IGenericRepository<T> _repository;
        // 2. Context'i çağırıyoruz (Kaydetme/SaveChanges işlemi için)
        private readonly AppDbContext _context;

        public Service(IGenericRepository<T> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync(); // Veritabanına asıl kayıt anı burası
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // Listeye çevirip dönüyoruz
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await _context.SaveChangesAsync(); // Silme işlemini onayla
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _context.SaveChangesAsync(); // Güncellemeyi onayla
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}