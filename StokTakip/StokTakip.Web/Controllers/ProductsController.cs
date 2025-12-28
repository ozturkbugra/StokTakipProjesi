using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StokTakip.Core;
using StokTakip.Web.Models;
using System.Text;

namespace StokTakip.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlApi;

        public ProductsController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _urlApi = configuration["ApiSettings:BaseUrl"];
        }

        public async Task<IActionResult> Index()
        {
            var model = new ProductIndexViewModel();

            // Ürünleri Çek
            var responseProd = await _httpClient.GetAsync(_urlApi + "api/products");
            if (responseProd.IsSuccessStatusCode)
            {
                var json = await responseProd.Content.ReadAsStringAsync();
                model.Products = JsonConvert.DeserializeObject<List<Product>>(json);
            }

            // Firmaları Çek (Dropdown için lazım)
            var responseComp = await _httpClient.GetAsync(_urlApi + "api/companies");
            if (responseComp.IsSuccessStatusCode)
            {
                var json = await responseComp.Content.ReadAsStringAsync();
                model.Companies = JsonConvert.DeserializeObject<List<Company>>(json);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_urlApi + "api/products", content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(_urlApi + "api/products", content);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync(_urlApi + $"api/products/{id}");
            return RedirectToAction("Index");
        }
    }
}