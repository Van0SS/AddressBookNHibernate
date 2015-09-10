using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using PersonsDB.Domain;

namespace AddressBookClientApp.Messages
{
    public class EditedPersonMessage : MessageBase
    {
        public Person Person { get; private set; }

        public EditedPersonMessage(Person person)
        {
            Person = person;
        }
    }
}
