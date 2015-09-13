using GalaSoft.MvvmLight.Messaging;
using PersonsDB.Domain;

namespace AddressBookClientApp.Message
{
    /// <summary>
    /// Запрос отредактировать контактные данные.
    /// </summary>
    public class SendPersonToEditMessage : MessageBase
    {
        public Person Person { get; set; }

        public SendPersonToEditMessage(Person person)
        {
            Person = person;
        }
    }
}
