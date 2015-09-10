using System;
using System.Collections.ObjectModel;
using AddressBookClientApp.Messages;
using AddressBookClientApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PersonsDB.Domain;

namespace AddressBookClientApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataPersons _dataPersons;

        /// <summary>
        /// The <see cref="Persons" /> property's name.
        /// </summary>
        public const string PersonsPropertyName = "Persons";

        private ObservableCollection<Person> _persons;

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set
            {
                if (_persons == value)
                    return;

                _persons = value;
                RaisePropertyChanged(PersonsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedPerson" /> property's name.
        /// </summary>
        public const string SelectedPersonPropertyName = "SelectedPerson";

        private Person _selectedPerson;

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                if (_selectedPerson == value)
                    return;

                _selectedPerson = value;
                RaisePropertyChanged(SelectedPersonPropertyName);
            }
        }

        public RelayCommand AddPersonCommand { get; private set; }

        public RelayCommand EditPersonCommand { get; private set; }

        public RelayCommand DeletePersonCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataPersons dataPersons)
        {
            _dataPersons = dataPersons;

            AddPersonCommand = new RelayCommand(AddPersonCommandExecute);
            EditPersonCommand = new RelayCommand(EditPersonCommandExecute);
            DeletePersonCommand = new RelayCommand(DeletePersonCommandExecute);

            Messenger.Default.Register<EditedPersonMessage>(this, EditedPersonMessageReceive);
            Messenger.Default.Register<PersonToAddMessage>(this, PersonToAddMessageReceive);

            Persons = new ObservableCollection<Person>(_dataPersons.GetAll());
            

            if (IsInDesignMode)
            {
                
                
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }

        }

        private void EditedPersonMessageReceive(EditedPersonMessage message)
        {
            _dataPersons.Update(message.Person);
            Persons = new ObservableCollection<Person>(_dataPersons.GetAll());
        }

        private void PersonToAddMessageReceive(PersonToAddMessage message)
        {
            _dataPersons.Add(message.Person);
            Persons = new ObservableCollection<Person>(_dataPersons.GetAll());
        }

        private void AddPersonCommandExecute()
        {
            Messenger.Default.Send(new RequestAddPersonMessage());

            Messenger.Default.Send(new ShowDialogViewMessage());
        }

        private void EditPersonCommandExecute()
        {
            if (SelectedPerson == null)
                return;
            Messenger.Default.Send(new SendPersonToEditMessage(SelectedPerson));
            Messenger.Default.Send(new ShowDialogViewMessage());
        }

        private void DeletePersonCommandExecute()
        {
            _dataPersons.Remove(SelectedPerson);
            Persons = new ObservableCollection<Person>(_dataPersons.GetAll());
        }

    }
}