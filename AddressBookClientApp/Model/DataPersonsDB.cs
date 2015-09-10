﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersonsDB.Domain;
using PersonsDB.Repositories;

namespace AddressBookClientApp.Model
{
    public class DataPersonsDB : IDataPersons
    {
        /*public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);
        }*/
        
        private IPersonRepository _personRepository = new PersonRepository();

        public void Add(Person person)
        {
            _personRepository.Add(person);
        }

        public void Update(Person person)
        {
            _personRepository.Update(person);
        }
        public void Remove(Person person)
        {
           _personRepository.Remove(person);
        }

        public Person GetById(Guid personId)
        {
            return _personRepository.GetById(personId);
        }

        public ICollection<Person> GetAll()
        {
            return _personRepository.GetAll();
        }
    }
}
