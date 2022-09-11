using PostgresCRUD.DataAccess;
using PostgresCRUD.DTOs;
using PostgresCRUD.GraphQL;
using PostgresCRUD.Interfaces;
using PostgresCRUD.Models;
using System.Security.Cryptography;
using System.Text;
using Path = System.IO.Path;

namespace PostgresCRUD.Services
{
    public class MutationService
    {

        private readonly ITokenService _tokenService;

        public MutationService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<PersonelSavePayLoad> CreatePersonel(PersonelSaveData data, [Service] postgresContext _context)
        {
            var personel = new Personels
            {
                Ad = data.Ad,
                Soyad = data.Soyad,
                Foto = data.Foto,
                MedeniDurum = data.MedeniDurum,
                KanGrup = data.KanGrup,
                BabaAd = data.BabaAd,
                AnaAd = data.AnaAd,
                BirimId = Int32.Parse(data.BirimId),
                GorevId = Int32.Parse(data.GorevId)
            };
            _context.Add(personel);
            await _context.SaveChangesAsync();
            return new PersonelSavePayLoad(personel);
        }

        public async Task<PersonelUpdatePayLoad> UpdatePersonel(PersonelUpdateData data, [Service] postgresContext _context)
        {
            var personel = new Personels
            {
                Id = Int32.Parse(data.id),
                Ad = data.Ad,
                Soyad = data.Soyad,
                Foto = data.Foto,
                MedeniDurum = data.MedeniDurum,
                KanGrup = data.KanGrup,
                BabaAd = data.BabaAd,
                AnaAd = data.AnaAd,
                BirimId = Int32.Parse(data.BirimId),
                GorevId = Int32.Parse(data.GorevId)
            };
            _context.Update(personel);
            await _context.SaveChangesAsync();
            return new PersonelUpdatePayLoad(personel);
        }

        public async Task<EgitimSavePayLoad> CreateEgitim(EgitimSaveData data, [Service] postgresContext _context)
        {
            var egitim = new Egitim
            {
                OkulId = Int32.Parse(data.OkulId),
                BolumId = Int32.Parse(data.BolumId),
                PersonelId = Int32.Parse(data.PersonelId),
                Tur = data.Tur,
                DiplomaNo = data.DiplomaNo,
                Mezuniyet = DateOnly.FromDateTime(Convert.ToDateTime(data.Mezuniyet))
            };
            _context.Add(egitim);
            await _context.SaveChangesAsync();
            return new EgitimSavePayLoad(egitim);
        }

        public async Task<EgitimUpdatePayLoad> UpdateEgitim(EgitimUpdateData data, [Service] postgresContext _context)
        {
            var egitim = new Egitim
            {
                Id = Int32.Parse(data.id),
                OkulId = Int32.Parse(data.OkulId),
                BolumId = Int32.Parse(data.BolumId),
                PersonelId = Int32.Parse(data.PersonelId),
                Tur = data.Tur,
                DiplomaNo = data.DiplomaNo,
                Mezuniyet = DateOnly.FromDateTime(Convert.ToDateTime(data.Mezuniyet))
            };
            _context.Update(egitim);
            await _context.SaveChangesAsync();
            return new EgitimUpdatePayLoad(egitim);
        }

        public async Task<Personels> DeletePersonel(string id, [Service] postgresContext _context)
        {
            var entity = _context.Personels.FirstOrDefault(t => t.Id.ToString().Equals(id));
            _context.Personels.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Egitim> DeleteEgitim(string id, [Service] postgresContext _context)
        {
            var entity = _context.Egitims.FirstOrDefault(t => t.Id.ToString().Equals(id));
            _context.Egitims.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserDto> Register(RegisterDto registerDto, [Service] postgresContext _context)
        {
            Query _query = new Query();
            if (await _query.UserExists(registerDto.Username, _context)) throw new Exception("UserName Is Already Taken");         //BadRequest("UserName Is Already Taken");
            var hmac = new HMACSHA512();

            var user = new Appuser
            {
                Username = registerDto.Username.ToLower(),
                Passwordhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                Passwordsalt = hmac.Key,

            };

            _context.Appusers.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        public UserDto Login(LoginDto loginDto, [Service] postgresContext _context)
        {

            Query _query = new Query();

            Appuser appuser = _query.getUser(loginDto.Username, _context).Result;

            if (appuser == null) throw new Exception("Invalid UserName");           //Unauthorized("Invalid UserName");

            var hmac = new HMACSHA512(appuser.Passwordsalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != appuser.Passwordhash[i]) throw new Exception("Invalid UserName");         //Unauthorized("Invalid Password");
            }

            return new UserDto
            {
                Username = appuser.Username,
                Token = _tokenService.CreateToken(appuser)
            };
        }

        public string Upload(IFile file)
        {
            string root = @"C:\react\project\public\assets\";
            string filePath = "";

            if (file.Length > 0)
            {
                if (!Directory.Exists(root))
                {

                    Directory.CreateDirectory(root);
                }
                string extension = Path.GetExtension(file.Name);
                string guid = Guid.NewGuid().ToString();
                filePath = guid + extension;

                using (FileStream fileStream = new FileStream(root + filePath, FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                    fileStream.Flush();
                    return filePath;
                }
            }
            return filePath;
        }
    }
}
