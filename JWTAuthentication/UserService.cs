using JWTAuthentication.Context;
using JWTAuthentication.DataModel;

namespace JWTAuthentication
{

    public interface IUserService
    {

        public bool AddUser(UserDMO user);
        public bool Login(UserDMO user);


    }
    public class UserService : IUserService
    {
        private UserContext _dbContext;
        public UserService(UserContext context)
        {
            _dbContext = context;
        }
        public bool AddUser(UserDMO user)
        {
            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Login(UserDMO user)
        {
            return _dbContext.Users.Any(s => s.Email == user.Email && s.Password == user.Password);

        }


    }
}
