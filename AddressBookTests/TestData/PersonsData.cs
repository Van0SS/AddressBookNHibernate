using System.Collections.Generic;
using PersonsDB.Domain;

namespace AddressBookTests.TestData
{
    public static class PersonsData
    {
        public static readonly List<Person> ListPersons
            = new List<Person>()
            {
                new Person()
                {
                    Name = "Ivan",
                    Surname = "Bolvan",
                    Nickname = "nagibator69",
                    MailAddress = "Pushkina st.",
                    ICQ = "666666",
                    Email = "i.bolvan@sevrer.com",
                    Phone = "78005553535",
                    Skype = "ivanoff"
                },
                new Person()
                {
                    Name = "Semen",
                    Surname = "Usmanov",
                    Email = "SemUsov@gmail.com"
                }
            };
    }
}
