using System.Collections.Generic;

namespace GlupiApp.Data
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<Person> CreatedPersons { get; set; }
    }
}