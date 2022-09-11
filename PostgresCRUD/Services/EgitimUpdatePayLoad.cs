using PostgresCRUD.Models;

namespace PostgresCRUD.Services
{
    public class EgitimUpdatePayLoad
    {
        public Egitim Egitim { get; }
        public EgitimUpdatePayLoad(Egitim egitim)
        {
            Egitim = egitim;
        }
    }
}
