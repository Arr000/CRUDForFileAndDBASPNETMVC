using DatingDataCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDB
{
    public interface IUserRepository
    {
        void Add(User user);
        List<UserDB> GetList();
        UserDB GetUserByEmail(string email);
        void Delete(string email);
        void Update(UserDB userdb);
    }
}
