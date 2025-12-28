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
    }
}