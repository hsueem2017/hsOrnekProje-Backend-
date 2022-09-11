using PostgresCRUD.Models;

namespace PostgresCRUD.Services
{
    public class EgitimSavePayLoad
    {
        public Egitim Egitim { get; }
        public EgitimSavePayLoad(Egitim egitim)
        {
            Egitim = egitim;
        }
    }
}
