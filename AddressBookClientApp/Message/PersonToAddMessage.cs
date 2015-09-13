using GalaSoft.MvvmLight.Messaging;
using PersonsDB.Domain;

namespace AddressBookClientApp.Message
{
    /// <summary>
    /// Сообщение содержащие новые контактные данные.
    /// </summary>
    public class PersonToAddMessage : MessageBase
    {
        public Person Person { get; private set; }

        public PersonToAddMessage(Person person)
        {
            Person = person;
        }
    }
}
