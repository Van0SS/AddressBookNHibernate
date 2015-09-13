using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using PersonsDB.DBException;
using PersonsDB.Domain;

namespace PersonsDB.Repositories
{
    public class NHibernateHelper
    {
        /// <summary>
        /// Путь к файлу БД.
        /// </summary>
        private static string DbFile = "AddressBook.sqlite";

        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Fluently.Configure()
                        .Database(SQLiteConfiguration.Standard
                            .UsingFile(DbFile)
                            .ShowSql() // Показывать запросы.
                            )
                        .Mappings(m => m
                            .FluentMappings.AddFromAssemblyOf<Person>())
                        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true)) 
                        .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static void DeleteDB()
        {
            try
            {
                File.Delete(DbFile);
            }
            catch (IOException exception)
            {
                throw new DeleteDBException("Can't delete DB", exception);
            }
                
            // Обнулить сессию, чтобы при создании новой сессии создалась новая БД.
            _sessionFactory = null;
        }
    }
}
