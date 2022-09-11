using System;
using System.Collections.Generic;

namespace PostgresCRUD.Models
{
    public partial class Gorev
    {
        public Gorev()
        {
            Personels = new HashSet<Personels>();
        }

        public int Id { get; set; }
        public string? Ad { get; set; }
        public int? Zindex { get; set; }

        public virtual ICollection<Personels> Personels { get; set; }
    }
}
