using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DatingDataCommon
{
    [XmlRoot("User")]
    public class User
    {
        [XmlElement("Id")]
        public int Id { get; set; }
       
        [XmlElement("Email")]
        public string Email { get; set; }
       
        [XmlElement("FirstName")]
        public string FirstName { get; set; }
        
        [XmlElement("LastName")]
        public string LastName { get; set; }
        
        [XmlElement("Country")]
        public string Country { get; set; }
        
        [XmlElement("Hobby")]
        public string Hobby { get; set; }

       
    }

    [XmlRoot("Users")]
    public class Users
    {
        [XmlArray("Users")]
        [XmlArrayItem("User", typeof(User))]
        public User[] users { get; set; }
    }

}
