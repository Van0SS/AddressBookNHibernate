using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersonsDB.Domain;

namespace AddressBookClientApp.Model
{
    /// <summary>
    /// Фейковые данные для отображения в дизайнере.
    /// </summary>
    public class DataPersonsDesign : IDataPersons
    {
        public void Add(Person person)
        {
            throw new NotImplementedException();
        }

        public void Update(Person person)
        {
            throw new NotImplementedException();
        }

        public void Remove(Person person)
        {
            throw new NotImplementedException();
        }

        public Person GetById(Guid personId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Person> GetAll()
        {
            return new List<Person>()
            {
                new Person()
                {
                    Name = "Ivan",
                    Surname = "Bolvan",
                    Nickname = "nagibator69",
                    MailAddress = "Pushkina st.",
                    ICQ = "666666",
                    Email = "i.bolvan@sevrer.com",
                    Phone = "+78005553535",
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
}
