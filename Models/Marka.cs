using System.Collections.Generic;

namespace TestProject.Models
{
public class Marka
    {
        public int MarkaId { get; set; }
        public string MarkaAdı { get; set; }
        public ICollection<Urun> Urun { get; set; }
    }
}