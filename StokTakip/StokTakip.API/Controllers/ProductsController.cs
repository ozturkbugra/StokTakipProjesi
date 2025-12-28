using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StokTakip.Core;
using StokTakip.Core.Interfaces;

namespace StokTakip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Service katmanıyla konuşacağız.
        // Repository ile DEĞİL! Hiyerarşi önemli: API -> Service -> Repository
        private readonly IService<Product> _service;

        public ProductsController(IService<Product> service)
        {
            _service = service;
        }

        // GET: api/products
        // Tüm ürünleri getirir
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            return Ok(products); // 200 OK koduyla veriyi döner
        }

        // POST: api/products
        // Yeni ürün ekler
        [HttpPost]
        public async Task<IActionResult> Save(Product product)
        {
            var newProduct = await _service.AddAsync(product);
            return Ok(newProduct);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            await _service.UpdateAsync(product);
            return NoContent(); // 204: İşlem başarılı ama geriye veri dönmüyorum demek (Standarttır)
        }

        // SİLME (DELETE)
        // Kullanımı: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            // Önce silinecek ürünü bulmamız lazım
            var product = await _service.GetByIdAsync(id);

            // Eğer ürün yoksa hata dön
            if (product == null)
            {
                return NotFound();
            }

            // Varsa sil
            await _service.RemoveAsync(product);
            return NoContent();
        }
    }
}