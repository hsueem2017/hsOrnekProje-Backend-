using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresCRUD.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Path = System.IO.Path;

namespace PostgresCRUD.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly postgresContext _context;

        public DataAccessProvider(postgresContext context)
        {
            _context = context;
        }

        public Personels GetSinglePersonel(int id)
        {
            var query = from k1 in _context.Kodlars
                        from k2 in _context.Kodlars
                        from b in _context.Birims
                        from g in _context.Gorevs
                        from p in _context.Personels.Where(p => k1.Id == p.MedeniDurum && k2.Id == p.KanGrup && p.Id == id && b.Id == p.BirimId && g.Id == p.GorevId)
                        select new Personels { Id = p.Id, Ad = p.Ad, Soyad = p.Soyad, Foto = p.Foto, MedeniDurum = p.MedeniDurum, KanGrup = p.KanGrup, BabaAd = p.BabaAd, AnaAd = p.AnaAd, BirimId = p.BirimId, GorevId = p.GorevId };

            return query.FirstOrDefault();
        }

        public IList GetAllPersonels()
        {
            var query = from k1 in _context.Kodlars
                        from k2 in _context.Kodlars
                        from p in _context.Personels.Where(p => k1.Id == p.MedeniDurum && k2.Id == p.KanGrup)
                            //select new { p, medeni_durum_ack = k1.KodAck, kan_grup_ack = k2.KodAck };
                        select new { p.Id, p.Ad, p.Soyad, p.Foto, p.MedeniDurum, medeni_durum_ack = k1.KodAck, p.KanGrup, kan_grup_ack = k2.KodAck, p.BabaAd, p.AnaAd, p.BirimId, p.GorevId };

            return query.ToList();
        }

        public IList GetAllEgitim(int id)
        {
            var query = _context.Egitims.Where(e => e.PersonelId == id);

            return query.ToList();
        }

        public IList Kodlar(string tablo)
        {
            var query = _context.Kodlars.Where(k => k.Tablo.Equals(tablo));

            return query.ToList();
        }

        public IList Birim()
        {
            var query = _context.Birims.ToList();

            return query;
        }

        public IList Gorev()
        {
            var query = _context.Gorevs.ToList();

            return query;
        }

        public IList Okul()
        {
            var query = _context.Okuls.ToList();

            return query;
        }

        public IList Bolum()
        {
            var query = _context.Bolums.ToList();

            return query;
        }

        public IList GetAllJoinRecords()
        {
            var query = from k1 in _context.Kodlars
                        from k2 in _context.Kodlars
                        from k3 in _context.Kodlars
                        from brm in _context.Birims
                        from g in _context.Gorevs
                        from o in _context.Okuls
                        from blm in _context.Bolums
                        from p in _context.Personels.Where(p => k1.Id == p.MedeniDurum && k2.Id == p.KanGrup && p.BirimId == brm.Id && p.GorevId == g.Id)
                        from e in _context.Egitims.Where(e => e.OkulId == o.Id && e.BolumId == blm.Id && e.PersonelId == p.Id && e.Tur == k3.Id)
                        select new { p, medeni_durum_ack = k1.KodAck, kan_grup_ack = k2.KodAck, birim_ad = brm.Ad, gorev_ad = g.Ad, okul_ad = o.Ad, bolum_ad = blm.Ad, okul_tur_ack = k3.KodAck };

            return query.ToList();
        }

        public void AddPersonel(Personels personeller)
        {
            _context.Personels.Add(personeller);
            _context.SaveChanges();
        }

        public void UpdatePersonel(Personels personel)
        {
            _context.Personels.Update(personel);
            _context.SaveChanges();
        }

        public void DeletePersonel(int id)
        {
            var entity = _context.Personels.FirstOrDefault(t => t.Id == id);
            _context.Personels.Remove(entity);
            _context.SaveChanges();
        }

        public void AddEgitim(Egitim egitimler)
        {
            _context.Egitims.Add(egitimler);
            _context.SaveChanges();
        }

        public void UpdateEgitim(Egitim egitim)
        {
            _context.Egitims.Update(egitim);
            _context.SaveChanges();
        }

        public void DeleteEgitim(int id)
        {
            var entity = _context.Egitims.FirstOrDefault(t => t.Id == id);
            _context.Egitims.Remove(entity);
            _context.SaveChanges();
        }

        public string Upload([FromForm(Name = "file")] IFormFile file)
        {
            string root = @"C:\react\project\public\assets\";
            string filePath = "";

            if (file.Length > 0)
            {
                if (!Directory.Exists(root))
                {                            
                                           
                    Directory.CreateDirectory(root);
                }
                string extension = Path.GetExtension(file.FileName);
                string guid = Guid.NewGuid().ToString();
                filePath = guid + extension;

                using (FileStream fileStream = new FileStream(root + filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                    return filePath;
                }
            }
            return filePath;
        }
    }
}
