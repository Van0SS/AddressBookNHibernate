using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersonsDB.Domain;

namespace AddressBookClientApp.Model
{
    public interface IDataPersons
    {
        void Add(Person person);
        void Update(Person person);
        void Remove(Person person);
        Person GetById(Guid personId);
        ICollection<Person> GetAll();
    }

}
