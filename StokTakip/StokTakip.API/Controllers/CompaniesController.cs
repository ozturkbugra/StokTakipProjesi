using Microsoft.AspNetCore.Mvc;
using StokTakip.Core;
using StokTakip.Core.Interfaces;

namespace StokTakip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IService<Company> _service;

        public CompaniesController(IService<Company> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _service.GetAllAsync();
            return Ok(companies);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            var newCompany = await _service.AddAsync(company);
            return Ok(newCompany);
        }
    }
}