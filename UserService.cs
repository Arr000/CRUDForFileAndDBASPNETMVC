using DatingDataCommon;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DataDB
{
    public class UserService : IUserService
    {
        private IUserRepository _userRep;
        public UserService(IUserRepository userRepository)
        {
            _userRep = userRepository;
        }
        public List<User> List()
        {
            var listOfUsers = _userRep.GetList();
            return listOfUsers.Select(udb => new User() { Email = udb.Email, Country = udb.Country, Id = udb.Id, FirstName = udb.FirstName, Hobby = udb.Hobby, LastName = udb.LastName }).ToList();
        }

        public User GetUserbyEmail(string email)
        {
            var userdb = _userRep.GetUserByEmail(email);
            return new User() { Email = userdb.Email, Country = userdb.Country, FirstName = userdb.FirstName,  Id = userdb.Id, LastName = userdb.LastName };
        }

        public void Update(User user)
        {
            _userRep.Update(new UserDB() {FirstName = user.FirstName, LastName = user.LastName, Country = user.Country, Email = user.Email, Hobby = user.Hobby, Id = user.Id });
        }

        public void Delete(string email)
        {
            _userRep.Delete(email);
        }

        public void Add(User user)
        {
            _userRep.Add(user);
        }

        string MakeDbString(string s)
        {
            return "'" + s + "'";
        }
    }
}
    

