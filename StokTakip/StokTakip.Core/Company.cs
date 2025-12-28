using System.Collections.Generic; // ICollection için gerekli

namespace StokTakip.Core
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string TaxNumber { get; set; } // Vergi No

        // İLİŞKİ KISMI:
        // Bir firmanın sattığı birden fazla ürün olabilir.
        // O yüzden buraya bir "List" (Koleksiyon) koyuyoruz.
        public ICollection<Product> Products { get; set; }
    }
}