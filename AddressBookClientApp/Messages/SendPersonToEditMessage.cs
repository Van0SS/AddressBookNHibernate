using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using PersonsDB.Domain;

namespace AddressBookClientApp.Messages
{
    public class SendPersonToEditMessage : MessageBase
    {
        public Person Person { get; set; }

        public SendPersonToEditMessage(Person person)
        {
            Person = person;
        }
    }
}
