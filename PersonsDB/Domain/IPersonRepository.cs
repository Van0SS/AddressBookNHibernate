using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonsDB.Domain
{
    public interface IPersonRepository
    {
        void Add(Person person);

        void AddMany(ICollection<Person> persons);

        void Update(Person person);

        void Remove(Person person);

        Person GetById(Guid personId);

        ICollection<Person> GetAll();

        long RowCount();
    }
}
