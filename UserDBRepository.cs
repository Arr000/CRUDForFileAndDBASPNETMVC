using DatingDataCommon;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataDB
{
    public class UserDBRepository : IUserRepository
    {
        private string connectionString;
        private SqlConnection cnn;
        private SqlCommand command;
        private SqlDataAdapter adapter = new SqlDataAdapter();
        SqlDataReader dataReader;

        public static String getConnection()
        {
            return "Server=(localdb)\\mssqllocaldb;Database=HobbyProjectDatingDB;Trusted_Connection=True";
        }

        public List<UserDB> GetList()
        {
            List<UserDB> listOfUsers = new List<UserDB>();
            string connectionString = UserDBRepository.getConnection();
            cnn = new SqlConnection(connectionString);

            string sql = "SELECT * FROM USERS";
            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                var userDB = new UserDB()
                {
                    FirstName = dataReader["FirstName"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    Country = dataReader["Country"].ToString(),
                    Hobby = dataReader["Hobby"].ToString(),
                    Email = dataReader["email"].ToString()

                };
                listOfUsers.Add(userDB);

            }
            cnn.Close();

            return listOfUsers;
        }

        public UserDB GetUserByEmail(string email)
        {
            connectionString = UserDBRepository.getConnection();
            cnn = new SqlConnection(connectionString);
            UserDB userDB = null;
            string sql = "SELECT * FROM USERS WHERE email = " + "'" + email + "'";
            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                userDB = new UserDB()
                {
                    FirstName = dataReader["FirstName"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    Country = dataReader["Country"].ToString(),
                    Hobby = dataReader["Hobby"].ToString(),
                    Email = dataReader["email"].ToString()
                };
            }

            return userDB;
        }

        public void Add(User user)
        {

            var userdb = new UserDB()
            {
                Email = user.Email,
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country,
                Hobby = user.Hobby
            };

            var query = "INSERT INTO USERS (FirstName, LastName, Country, Hobby, email) Values (" + MakeDbString1(userdb.FirstName) +
                  "," + MakeDbString1(userdb.LastName) + "," + MakeDbString1(userdb.Country) + "," +
                  MakeDbString1(userdb.Hobby) + "," + MakeDbString1(userdb.Email) + ")";


            Console.WriteLine("User is inserted in DB");
            connectionString = UserDBRepository.getConnection();
            cnn = new SqlConnection(query);
            cnn.Open();

            command = new SqlCommand(query, cnn);
            adapter.InsertCommand = new SqlCommand(query, cnn);
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            cnn.Close();
        }

        public void Update(UserDB userdb)
        {
            connectionString = UserDBRepository.getConnection();
            adapter = new SqlDataAdapter();

            var sql = "UPDATE USERS SET FIRSTNAME = " + MakeDbString1(userdb.FirstName) + ", LastName = " + MakeDbString1(userdb.LastName) + ", Country = " + MakeDbString1(userdb.Country) + ", Hobby = " + MakeDbString1(userdb.Hobby)
                + ", email = " + MakeDbString1(userdb.Email) + " WHERE email = " + MakeDbString1(userdb.Email);

            var cnn = new SqlConnection(connectionString);
            cnn.Open();

            var command = new SqlCommand(sql, cnn);
            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();

            command.Dispose();
            cnn.Close();
        }

        public String MakeDbString1(string s)
        {
            return "'" + s + "'";
        }

        public void Delete(string email)
        {
            connectionString = UserDBRepository.getConnection();
            SqlDataAdapter adapter = new SqlDataAdapter();

            var sql = "DELETE FROM USERS WHERE email = " + MakeDbString1(email);

            var cnn = new SqlConnection(connectionString);
            cnn.Open();

            var command = new SqlCommand(sql, cnn);

            adapter.DeleteCommand = new SqlCommand(sql, cnn);
            adapter.DeleteCommand.ExecuteNonQuery();

            command.Dispose();
            cnn.Close();
        }
    }


}
