using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using AddressBookClientApp.Messages;
using AddressBookClientApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PersonsDB.Domain;

namespace AddressBookClientApp.ViewModel
{
    public class DialogViewModel : ViewModelBase
    {

        /// <summary>
        /// The <see cref="AcceptingText" /> property's name.
        /// </summary>
        public const string AcceptingTextPropertyName = "AcceptingText";

        private string _acceptingText = "Add";

        public string AcceptingText
        {
            get
            {   return _acceptingText;  }

            set
            {
                if (_acceptingText == value)
                    return;

                _acceptingText = value;
                RaisePropertyChanged(AcceptingTextPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="EditPerson" /> property's name.
        /// </summary>
        public const string EditPersonPropertyName = "EditPerson";

        private Person _editPerson;

        public Person EditPerson
        {
            get
            {
                return _editPerson;
            }

            set
            {
                if (_editPerson == value)
                    return;

                _editPerson = value;

                RaisePropertyChanged(EditPersonPropertyName);
            }
        }

        public RelayCommand AcceptChangesCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public DialogViewModel()
        {
            Messenger.Default.Register<RequestAddPersonMessage>(this, RequestAddPersonRecieve);
            Messenger.Default.Register<SendPersonToEditMessage>(this, PersonToEditRecieve);

            AcceptChangesCommand = new RelayCommand(AcceptChangesCommandCommandExecute);
            CancelCommand = new RelayCommand(() => Messenger.Default.Send(new HideDialogViewMessage()));
        }

        private void PersonToEditRecieve(SendPersonToEditMessage message)
        {
            AcceptingText = "Edit";
            EditPerson = (Person) message.Person.Clone();
        }

        private void RequestAddPersonRecieve(RequestAddPersonMessage message)
        {
            AcceptingText = "Add";
            EditPerson = new Person() {Name = "Name", Surname = "Surname"};
        }

        private void AcceptChangesCommandCommandExecute()
        {
            if (AcceptingText == "Add")
            {
                Messenger.Default.Send(new PersonToAddMessage(EditPerson));
            }
            else
            {
                Messenger.Default.Send(new EditedPersonMessage(EditPerson));
            }

            Messenger.Default.Send(new HideDialogViewMessage());
        }


    }
}
