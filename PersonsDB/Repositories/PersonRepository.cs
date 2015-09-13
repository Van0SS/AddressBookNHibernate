using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using PersonsDB.DBException;
using PersonsDB.Domain;

namespace PersonsDB.Repositories
{
    /// <summary>
    /// Взаимодействие с таблицей Person.
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        /// <summary>
        /// Добавить человека в таблицу.
        /// </summary>
        public void Add(Person person)
        {
            // Открыть сессию взаимодействия с БД.
            using (ISession session = NHibernateHelper.OpenSession())
            {
                // Начать транзакцию с БД.
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(person);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Добавить несколько людей в БД.
        /// </summary>
        /// <param name="persons">Коллекция людей</param>
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

        /// <summary>
        /// Обновить человека в БД.
        /// </summary>
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

        /// <summary>
        /// Удалить человека.
        /// </summary>
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

        /// <summary>
        /// Выбрать человека из БД по ИД.
        /// </summary>
        public Person GetById(Guid personId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<Person>(personId);
            }
        }

        /// <summary>
        /// Извлечь все записи таблицы из БД.
        /// </summary>
        /// <returns></returns>
        public ICollection<Person> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                try
                {
                    return session.CreateCriteria(typeof(Person)).List<Person>();
                }
                catch (ADOException exception)
                {
                    throw new ReadDBException("Can't read table Person", exception);
                }
                
            }
        }

        /// <summary>
        /// Получить количество записей в таблице.
        /// </summary>
        public long RowCount()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<Person>().RowCountInt64();
            }
        }
    }
}
