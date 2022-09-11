using PostgresCRUD.Models;
using System.Collections;
using System.Collections.Generic;

namespace PostgresCRUD.DataAccess
{
    public interface IDataAccessProvider
    {
        IList GetAllPersonels();
        Personels GetSinglePersonel(int id);
        IList GetAllJoinRecords();
        IList GetAllEgitim(int id);
        IList Kodlar(string tablo);
        IList Birim();
        IList Gorev();
        IList Okul();
        IList Bolum();
        void AddPersonel(Personels patient);
        void UpdatePersonel(Personels personel);
        void DeletePersonel(int id);
        void AddEgitim(Egitim egitim);
        void UpdateEgitim(Egitim egitim);
        void DeleteEgitim(int id);
        string Upload(IFormFile file);


        //void UpdatePatientRecord(Patient patient);
        //void DeletePatientRecord(string id);
    }
}