using System;
using System.Collections.Generic;

namespace PostgresCRUD.Models
{
    public partial class Bolum
    {
        public Bolum()
        {
            Egitims = new HashSet<Egitim>();
        }

        public int Id { get; set; }
        public string? Ad { get; set; }

        public virtual ICollection<Egitim> Egitims { get; set; }
    }
}
