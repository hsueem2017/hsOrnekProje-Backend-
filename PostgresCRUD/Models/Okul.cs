
namespace PostgresCRUD.Models
{
    public partial class Okul
    {
        public Okul()
        {
            Egitims = new HashSet<Egitim>();
        }

        public int Id { get; set; }
        public string? Ad { get; set; }

        public virtual ICollection<Egitim> Egitims { get; set; }
    }
}
