using System;
using System.Collections.Generic;

namespace PostgresCRUD.Models
{
    public partial class Kodlar
    {
        public Kodlar()
        {
            Egitims = new HashSet<Egitim>();
            PersonelKanGrupNavigations = new HashSet<Personels>();
            PersonelMedeniDurumNavigations = new HashSet<Personels>();
        }

        public string Id { get; set; } = null!;
        public string? Tablo { get; set; }
        public string? Kod { get; set; }
        public string? KodAck { get; set; }
        public bool? Isaktif { get; set; }

        public virtual ICollection<Egitim> Egitims { get; set; }
        public virtual ICollection<Personels> PersonelKanGrupNavigations { get; set; }
        public virtual ICollection<Personels> PersonelMedeniDurumNavigations { get; set; }
    }
}
