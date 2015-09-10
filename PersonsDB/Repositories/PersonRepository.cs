using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using PersonsDB.Domain;

namespace PersonsDB.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public void Add(Person person)
        {
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(person);
                        transaction.Commit();
                    }
                }
            }
        }

        public void AddMany(ICollection<Person> persons)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    foreach (var person in persons)
                    {
                        session.Save(person);
                        
                    }
                    transaction.Commit();
                }
            }
        }

        public void Update(Person person)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(person);
                    transaction.Commit();
                }
            }
        }

        public void Remove(Person person)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(person);
                    transaction.Commit();
                }
            }
        }

        public Person GetById(Guid personId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<Person>(personId);
        }

       /* public Person GetByName(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Person product = session
                    .CreateCriteria(typeof(Person))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Person>();
                return product;
            }
        }*/

        public ICollection<Person> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var a = session.CreateCriteria(typeof(Person)).List<Person>();
                return a;

            }
        }

        public long RowCount()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<Person>().RowCountInt64();
            }
        }
    }
}
