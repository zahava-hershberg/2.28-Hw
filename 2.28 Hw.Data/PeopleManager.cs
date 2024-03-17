using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._28_Hw.Data
{
    public class People
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set;}
        public int Age { get; set; }
    }
    public class PeopleViewModel
    {
        public List<People> People { get; set; }
        public string Message { get; set; }
    }
    public class PeopleManager
    {
        private string _connectionString;
        public PeopleManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<People> GetPeople()
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            var people = new List<People>();
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                people.Add(new People
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            return people;
        }
        public void AddPeople(People p)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd=connection.CreateCommand();
            cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) VALUES (@firstName, @lastName, @age)";
            cmd.Parameters.AddWithValue("@firstName", p.FirstName);
            cmd.Parameters.AddWithValue("@lastName", p.LastName);
            cmd.Parameters.AddWithValue("@age", p.Age);
            connection.Open();
            cmd.ExecuteNonQuery();

        }
        public void AddMany(List<People>people)
        {
            foreach(var p in people)
            {
                AddPeople(p);
            }
        }
    }
}
