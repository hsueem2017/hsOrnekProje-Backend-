using System;
using System.Collections.Generic;

namespace PostgresCRUD.Models
{
    public partial class Appuser
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public byte[] Passwordhash { get; set; } = null!;
        public byte[] Passwordsalt { get; set; } = null!;
    }
}
