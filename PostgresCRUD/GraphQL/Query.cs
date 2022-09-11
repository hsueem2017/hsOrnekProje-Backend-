using Microsoft.EntityFrameworkCore;
using PostgresCRUD.DataAccess;
using PostgresCRUD.Models;

namespace PostgresCRUD.GraphQL
{

    public class Query
    {

        [UseProjection]
        public Personels GetSinglePersonel([Service] postgresContext _context, int id)
        {
            var query = _context.Personels.Where(e => e.Id == id);

            return query.FirstOrDefault();
        }

        [UseProjection]
        public IQueryable<Personels> GetAllPersonel([Service] postgresContext _context)
        {
            return _context.Personels;
        }

        [UseProjection]
        public IQueryable<Egitim> GetAllEgitim([Service] postgresContext _context, int id)
        {
            var query = _context.Egitims.Where(e => e.PersonelId == id);

            return query;
        }

        [UseProjection]
        public IQueryable<Kodlar> Kodlar([Service] postgresContext _context, string tablo)
        {
            var query = _context.Kodlars.Where(k => k.Tablo.Equals(tablo));

            return query;
        }

        [UseProjection]
        public IQueryable<Birim> Birim([Service] postgresContext _context)
        {
            return _context.Birims;
        }

        [UseProjection]
        public IQueryable<Gorev> Gorev([Service] postgresContext _context)
        {
            return _context.Gorevs;
        }

        [UseProjection]
        public IQueryable<Okul> Okul([Service] postgresContext _context)
        {
            return _context.Okuls;
        }

        [UseProjection]
        public IQueryable<Bolum> Bolum([Service] postgresContext _context)
        {
            return _context.Bolums;
        }

        [UseProjection]
        public async Task<bool> UserExists(string username, [Service] postgresContext _context)
        {
            return await _context.Appusers.AnyAsync(x => x.Username == username.ToLower());
        }

        [UseProjection]
        public async Task<Appuser> getUser(string username, [Service] postgresContext _context)
        {
            return await _context.Appusers
                .SingleOrDefaultAsync(x => x.Username == username);
        }

        /*public IList GetAllJoinRecords([Service] postgresContext _context)
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
        }*/
    }
}
