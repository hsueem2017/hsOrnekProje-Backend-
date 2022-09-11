using PostgresCRUD.Models;

namespace PostgresCRUD.Interfaces
{
   public  interface ITokenService
    {
        string CreateToken(Appuser user);
    }
}
