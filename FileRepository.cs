using DataDB;
using DatingDataCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

using System.Runtime.Serialization;
using System.Xml.Serialization;
using RestSharp.Deserializers;
using System.Xml;

namespace FileStorage
{
    public class FileRepository : IUserRepository
    {
        public void Add(User user)
        {
            List<User> users = new List<User>();
            users.Add(user);

            if (!File.Exists("userinfo.txt"))
            {
                Stream stream = File.OpenWrite(Environment.CurrentDirectory + "\\userinfo.txt");
                XmlSerializer xmlserializer = new XmlSerializer(typeof(List<User>));
                xmlserializer.Serialize(stream, users);

                stream.Close();
            }
            else
            {

                using (var reader = new StreamReader("userinfo.txt"))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<User>));
                    users = (List<User>)deserializer.Deserialize(reader);
                    reader.Close();

                    users.Add(user);


                    Stream stream = File.OpenWrite(Environment.CurrentDirectory + "\\userinfo.txt");
                    XmlSerializer xmlserializer = new XmlSerializer(typeof(List<User>));
                    xmlserializer.Serialize(stream, users);

                    stream.Close();

                }
            }
        }

        public void Delete(string email)
        {
            
            User usertoSerialize = new User();
            List<User> ListOfUserDb = new List<User>();
            var users = GetList();
            File.Delete("userinfo.txt");
            foreach (var user in users)
            {
                if (user.Email.Equals(email))
                {
                    Console.WriteLine("User is not added in list");
                }
                else
                {
                    usertoSerialize.Id = user.Id;
                    usertoSerialize.FirstName = user.FirstName;
                    usertoSerialize.LastName = user.LastName;
                    usertoSerialize.Hobby = user.Hobby;
                    usertoSerialize.Email = user.Email;
                    usertoSerialize.Country = user.Country;

                    Add(usertoSerialize);
                }
            }
           
            Console.WriteLine("User is deleted");
        }

        public List<UserDB> GetList()
        {
            List<UserDB> listofUsersDB = new List<UserDB>();
            using (var reader = new StreamReader("userinfo.txt"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<User>));
                var users = (List<User>)deserializer.Deserialize(reader);
                reader.Close();
                
                foreach(var u in users)
                {
                    var userdb = new UserDB() { Country = u.Country, Email = u.Email, FirstName = u.FirstName, Hobby = u.Hobby, Id = u.Id, LastName = u.LastName };

                    listofUsersDB.Add(userdb);
                }

            }
            return listofUsersDB;
        }
        public UserDB GetUserByEmail(string email)
        {
            var users = GetList();

            ////var user = from u in users where u.Email == email select u;
            //////var user = users.Where(u => u.Email == email).FirstOrDefault();
            //var user = users.SingleOrDefault(u => u.Email == email);
            return users.FirstOrDefault(u => u.Email == email);
        }

        
        public void Update(UserDB userdb)
        {

            User usertoSerialize = new User();
            List<User> ListOfUser = new List<User>();
            var users = GetList();
            File.Delete("userinfo.txt");
            foreach (var user in users)
            {
                if (user.Email.Equals(userdb.Email))
                {
                    usertoSerialize.Id = userdb.Id;
                    usertoSerialize.FirstName = userdb.FirstName;
                    usertoSerialize.LastName = userdb.LastName;
                    usertoSerialize.Hobby = userdb.Hobby;
                    usertoSerialize.Email = userdb.Email;
                    usertoSerialize.Country = userdb.Country;

                    Add(usertoSerialize);
               }
                else
                {

                    usertoSerialize.Id = user.Id;
                    usertoSerialize.FirstName = user.FirstName;
                    usertoSerialize.LastName = user.LastName;
                    usertoSerialize.Hobby = user.Hobby;
                    usertoSerialize.Email = user.Email;
                    usertoSerialize.Country = user.Country;

                    Add(usertoSerialize);
                }
                
            }

            Console.WriteLine("User is updated");
        }


  

      
    } 
}

    
