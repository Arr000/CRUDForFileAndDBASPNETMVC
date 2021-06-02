using System.Collections.Generic;

namespace DatingDataCommon
{
    public interface IUserService
    {
        void Add(User user);

        List<User> List();

        User GetUserbyEmail(string email);

        void Update(User user);

        void Delete(string email);

    }
}
