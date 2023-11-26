using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.DataModel
{
    public class UserDMO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
