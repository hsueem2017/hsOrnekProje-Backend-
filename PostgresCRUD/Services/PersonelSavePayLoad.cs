using PostgresCRUD.Models;

namespace PostgresCRUD.Services
{
    public class PersonelSavePayLoad 
    {
        public Personels Personel { get; }
        public PersonelSavePayLoad(Personels personel)
        {
            Personel = personel;
        }
    }
}
