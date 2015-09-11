using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AddressBookTests.TestData;
using AddressBookTests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using PersonsDB.Domain;
using PersonsDB.Repositories;

namespace AddressBookTests.AddressBookDBTests
{
    [TestClass]
    public class PersonRepositoryTests
    {
        private readonly IPersonRepository _personRepository = new PersonRepository();

        [TestInitialize]
        public void BeforeEveryTestDeleteDataBase()
        {
            NHibernateHelper.DeleteDB();
        }

        [TestMethod]
        public void Add_AddPerson_DBHaveOneRow()
        {
            var person = PersonsData.ValidPersons[0];
            _personRepository.Add(person);
            Assert.AreEqual(1, _personRepository.RowCount());
        }

        [TestMethod]
        public void Add_AddEmptyNamePerson_InvokePropertyValueException()
        {
            var person = new Person() {Surname = "B"};

            MyAssert.Throws<PropertyValueException>( () => _personRepository.Add(person) );
        }

        [TestMethod]
        public void Add_AddEmptySurnamePerson_InvokePropertyValueException()
        {
            var person = new Person() { Name = "A" };

            MyAssert.Throws<PropertyValueException>(() => _personRepository.Add(person));
        }

        [TestMethod]
        public void AddMany_AddPersons_DBRowsAreEqualListCount()
        {
            var persons = PersonsData.ValidPersons;
            _personRepository.AddMany(persons);
            Assert.AreEqual(persons.Count, _personRepository.RowCount());
        }

        [TestMethod]
        public void GetById_AddPerson_CanGetPerson_PersonsAreEqual()
        {
            var person = PersonsData.ValidPersons[0];
            _personRepository.Add(person);

            var personDb = _personRepository.GetById(person.Id);

            Assert.IsNotNull(personDb);
            Assert.AreNotSame(person, personDb);
            Assert.AreEqual(person.Name, personDb.Name);
            Assert.AreEqual(person.Surname, personDb.Surname);
            Assert.AreEqual(person.Nickname, personDb.Nickname);
            Assert.AreEqual(person.MailAddress, personDb.MailAddress);
            Assert.AreEqual(person.Email, personDb.Email);
            Assert.AreEqual(person.ICQ, personDb.ICQ);
            Assert.AreEqual(person.Phone, personDb.Phone);
            Assert.AreEqual(person.Skype, personDb.Skype);
        }

        [TestMethod]
        public void Update_AddPerson_ChangePerson_PersonChanged()
        {
            var person = PersonsData.ValidPersons[0];
            _personRepository.Add(person);

            person = _personRepository.GetById(person.Id);
            person.Name = "Test";
            _personRepository.Update(person);

            Assert.AreEqual(1, _personRepository.RowCount());
            Assert.AreEqual("Test", _personRepository.GetById(person.Id).Name);
        }

        [TestMethod]
        public void Remove_AddPerson_PersonDeleted()
        {
            var person = PersonsData.ValidPersons[0];
            _personRepository.Add(person);
            Assert.AreEqual(1, _personRepository.RowCount());

            _personRepository.Remove(person);
            Assert.AreEqual(0, _personRepository.RowCount());
        }

        [TestMethod]
        public void GetAll_AddPersons_PersonsNameAreEqual()
        {
            var persons = PersonsData.ValidPersons;
            _personRepository.AddMany(persons);

            var personsDb = _personRepository.GetAll().ToList();
            for (int i = 0; i < persons.Count; i++)
            {
                Assert.AreEqual(persons[i].Name, personsDb[i].Name);
            }
        }

        [ClassCleanup]
        public static void DeleteDatabaseAfterTests()
        {
            NHibernateHelper.DeleteDB();
        }
    }
}
