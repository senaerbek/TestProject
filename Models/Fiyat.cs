namespace TestProject.Models
{
    public class Fiyat
    {
         public int FiyatId { get; set; }
        public decimal UrunFiyat { get; set; }
        public Urun Urun { get; set; }
    }
}