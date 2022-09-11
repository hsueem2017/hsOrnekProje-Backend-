using System;
using System.Collections.Generic;

namespace PostgresCRUD.Models
{
    public partial class Personels
    {
        public Personels()
        {
            Egitims = new HashSet<Egitim>();
        }

        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        public string Soyad { get; set; } = null!;
        public string? Foto { get; set; }
        public string? MedeniDurum { get; set; }
        public string? KanGrup { get; set; }
        public string? BabaAd { get; set; }
        public string? AnaAd { get; set; }
        public int? BirimId { get; set; }
        public int? GorevId { get; set; }

        public virtual Birim? Birim { get; set; }
        public virtual Gorev? Gorev { get; set; }
        public virtual Kodlar? KanGrupNavigation { get; set; }
        public virtual Kodlar? MedeniDurumNavigation { get; set; }
        public virtual ICollection<Egitim> Egitims { get; set; }
    }
}
