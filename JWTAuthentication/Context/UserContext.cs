using JWTAuthentication.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace JWTAuthentication.Context
{
    public class UserContext : DbContext
    {
        public DbSet<UserDMO> Users { get; set;}
        public UserContext(DbContextOptions option) : base(option)
        {


        }

        // context hazırladıktan sonra, nugetten,Microsoft.EntitiyFrameworkCore.Tools paketini indiriyorsunuz
        // kodlar yazıldıktan sonra,  add-migration init yazıyoruz.
        // ekrana migration dosyası çıkıyor. değişiklik yapmak istersek yapıyoruz. Db^nin oluşmasını sağlamak için update-database
    }
}
