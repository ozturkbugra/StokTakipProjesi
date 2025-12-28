namespace StokTakip.Core
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // --- İLİŞKİ KISMI ---

        // 1. Foreign Key (Yabancı Anahtar): Veritabanında tutulacak Id
        public int CompanyId { get; set; }

        // 2. Navigation Property (Gezinme Özelliği): 
        // Kod yazarken product.Company.Name diyebilmemizi sağlayan nesne.
        public Company? Company { get; set; }
    }
}