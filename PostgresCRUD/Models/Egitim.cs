using System;
using System.Collections.Generic;

namespace PostgresCRUD.Models
{
    public partial class Egitim
    {
        public int Id { get; set; }
        public int OkulId { get; set; }
        public int BolumId { get; set; }
        public int PersonelId { get; set; }
        public string? Tur { get; set; }
        public string? DiplomaNo { get; set; }
        public DateOnly? Mezuniyet { get; set; }

        public virtual Bolum Bolum { get; set; } = null!;
        public virtual Okul Okul { get; set; } = null!;
        public virtual Personels Personel { get; set; } = null!;
        public virtual Kodlar? TurNavigation { get; set; }
    }
}
