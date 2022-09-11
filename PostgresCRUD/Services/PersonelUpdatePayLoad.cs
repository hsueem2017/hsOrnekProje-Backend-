using PostgresCRUD.Models;

namespace PostgresCRUD.Services
{
    public class PersonelUpdatePayLoad
    {
        public Personels Personel { get; }
        public PersonelUpdatePayLoad(Personels personel)
        {
            Personel = personel;
        }
    }
}
