using StokTakip.Core;

namespace StokTakip.Web.Models
{
    public class ProductIndexViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Company> Companies { get; set; } = new List<Company>();
    }
}