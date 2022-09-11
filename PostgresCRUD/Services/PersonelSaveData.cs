using PostgresCRUD.Models;

namespace PostgresCRUD.Services
{
    public record PersonelSaveData (string Ad, string Soyad, string Foto, string MedeniDurum, string KanGrup, string BabaAd, string AnaAd, string BirimId, string GorevId);
}
