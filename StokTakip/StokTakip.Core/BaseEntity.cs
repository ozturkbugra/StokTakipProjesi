namespace StokTakip.Core
{
    // Ast sınıf olarak tanımlanmıştır, bu yüzden doğrudan örneği oluşturulamaz.
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now; // Oluştuğu an saati otomatik alsın
    }
}
