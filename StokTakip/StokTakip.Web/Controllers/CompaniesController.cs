using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StokTakip.Core;
using System.Text;

namespace StokTakip.Web.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlApi;

        public CompaniesController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _urlApi = configuration["ApiSettings:BaseUrl"];
        }

        // LİSTELEME
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_urlApi + "api/companies");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<Company>>(json);
                return View(values);
            }
            return View(new List<Company>());
        }

        // EKLEME (POST)
        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            var json = JsonConvert.SerializeObject(company);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // API'ye gönder
            await _httpClient.PostAsync(_urlApi + "api/companies", content);

            return RedirectToAction("Index");
        }

        // GÜNCELLEME (POST)
        [HttpPost]
        public async Task<IActionResult> Update(Company company)
        {
            var json = JsonConvert.SerializeObject(company);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // API'de PUT metodunu çağırıyoruz
            await _httpClient.PutAsync(_urlApi + "api/companies", content);

            return RedirectToAction("Index");
        }

        // SİLME
        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync(_urlApi + $"api/companies/{id}");
            return RedirectToAction("Index");
        }
    }
}