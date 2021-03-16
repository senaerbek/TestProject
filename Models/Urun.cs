using System.Collections.Generic;

namespace TestProject.Models
{
    public class Urun
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public Marka Marka { get; set; }
        public ICollection<Fiyat> Fiyat { get; set; }
    }
}