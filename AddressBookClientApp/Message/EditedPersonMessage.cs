using GalaSoft.MvvmLight.Messaging;
using PersonsDB.Domain;

namespace AddressBookClientApp.Message
{
    /// <summary>
    /// Сообщение содержащее отредактированные данные.
    /// </summary>
    public class EditedPersonMessage : MessageBase
    {
        public Person Person { get; private set; }

        public EditedPersonMessage(Person person)
        {
            Person = person;
        }
    }
}
