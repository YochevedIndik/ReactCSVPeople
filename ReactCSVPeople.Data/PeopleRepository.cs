using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReactCSVPeople.Data
{
    public class PeopleRepository
    {
        private readonly string _connectionString;
        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddPeople(List<Person> people)
        {
            var context = new PeopleDataContext(_connectionString);
            context.People.AddRange(people);
            context.SaveChanges();
        }
        public List<Person> GetAllPeople()
        {
            var context = new PeopleDataContext(_connectionString);
            return context.People.ToList();
        }
        public void DeleteAll()
        {
            var context = new PeopleDataContext(_connectionString);
            context.Database.ExecuteSqlRaw("DELETE FROM People");
        }
    }
}
