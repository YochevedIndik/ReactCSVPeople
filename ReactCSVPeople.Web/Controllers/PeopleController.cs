using CsvHelper;
using Faker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReactCSVPeople.Data;
using ReactCSVPeople.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactCSVPeople.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly string _connectionString;
        public PeopleController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [HttpPost]
        [Route("upload")]
        public void Upload(UploadViewModel vm)
        {
            int index = vm.Base64File.IndexOf(",") + 1;
            string base64 = vm.Base64File.Substring(index);
            byte[] bytes = Convert.FromBase64String(base64);
            var people = GetFromBytes(bytes);
            var repo = new PeopleRepository(_connectionString);
            repo.AddPeople(people);
        }
        [HttpGet]
        [Route("generatepeople")]
        public IActionResult Generate(int amount)
        {
            var people = GeneratePeople(amount);
            var csv = GenerateCSV(people);
            byte[] bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "people.csv");
        }
        [HttpGet]
        [Route("getpeople")]
        public List<Person> GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetAllPeople();
        }
        [HttpPost]
        [Route("delete")]
        public void DeleteAll()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeleteAll();
        }
        private List<Person> GeneratePeople(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ => new Person
            {
                FirstName = Name.First(),
                LastName = Name.Last(),
                Age = RandomNumber.Next(15, 100),
                Email = Internet.Email(),
                Address = Address.StreetAddress()
            }).ToList();
            
        }
        private string GenerateCSV(List<Person> people)
        {
            var builder = new StringBuilder();
            using var writer = new StringWriter(builder);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            return builder.ToString();
        }
        private List<Person> GetFromBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<Person>().ToList();
        }
    }
}
