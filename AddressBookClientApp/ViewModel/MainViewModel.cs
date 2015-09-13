using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using AddressBookClientApp.Message;
using AddressBookClientApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PersonsDB.DBException;
using PersonsDB.Domain;

namespace AddressBookClientApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataPersons _dataPersons;

        #region -- Bindable changeable properties --

        /// <summary>
        /// The <see cref="Persons" /> property's name.
        /// </summary>
        public const string PersonsPropertyName = "Persons";

        private ObservableCollection<Person> _persons;
        /// <summary>
        /// Контактные данные.
        /// </summary>
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

        /// <summary>
        /// Контактные данные выбранного человека.
        /// </summary>
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

        #endregion -- Bindable changeable properties --

        #region -- Commands --

        public ICommand AddPersonCommand { get; private set; }

        public ICommand EditPersonCommand { get; private set; }

        public ICommand DeletePersonCommand { get; private set; }

        #endregion -- Commands --

        #region -- Constructor --

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataPersons dataPersons)
        {
            _dataPersons = dataPersons;

            // Назначить команду для выполения.
            AddPersonCommand = new RelayCommand(AddPersonCommandExecute);

            // Назначить команду для выполения, а также команду для проверки на возможность выполнения.
            EditPersonCommand = new RelayCommand(EditPersonCommandExecute, EditPersonCommandCanExecute);

            DeletePersonCommand = new RelayCommand(DeletePersonCommandExecute, DeletePersonCommandCanExecute);

            // Зарегистрировать получение сообщений.
            Messenger.Default.Register<EditedPersonMessage>(this, EditedPersonMessageReceive);
            Messenger.Default.Register<PersonToAddMessage>(this, PersonToAddMessageReceive);

            try
            {
                RefreshPersons();
            }
            catch (Exception exception)
            {
                // Если произошла ошибка считывания данных из бд, то создать пустую коллекцию, ибо потом БД пересоздасться.
                if (exception is ReadDBException)
                    Persons = new ObservableCollection<Person>();

                // Вызов исключения в UI потоке вручную, потому что инициализация MainViewModel
                // происходит через ServiceLocator.
                Application.Current.Dispatcher.BeginInvoke(
                    new Action(() =>{ throw exception; }));
            }
        }

        #endregion -- Constructor --

        #region -- Messege receivers --

        /// <summary>
        /// Приём сообщения о необходимости изменить запись в источнике данных.
        /// </summary>
        private void EditedPersonMessageReceive(EditedPersonMessage message)
        {
            _dataPersons.Update(message.Person);
            RefreshPersons();
        }

        /// <summary>
        /// Приём сообщения о необходимости добавить запись в источник данных.
        /// </summary>
        private void PersonToAddMessageReceive(PersonToAddMessage message)
        {
            _dataPersons.Add(message.Person);
            RefreshPersons();
        }

        #endregion -- Message receivers --

        #region -- Command executers --

        private void AddPersonCommandExecute()
        {
            // Передать запрос на создание записи.
            Messenger.Default.Send(new RequestAddPersonMessage());

            // Показать форму добавления записи.
            Messenger.Default.Send(new ShowDialogViewMessage());
        }

        private void EditPersonCommandExecute()
        {
            if (SelectedPerson == null)
                return;

            // Передать запрос на редактирование записи.
            Messenger.Default.Send(new SendPersonToEditMessage(SelectedPerson));

            // Показать форму редактирования записи.
            Messenger.Default.Send(new ShowDialogViewMessage());
        }

        /// <summary>
        /// Проверка на возможность отредактировать запись.
        /// </summary>
        private bool EditPersonCommandCanExecute()
        {
            // Если есть выбранная запись, то можно редактировать.
            return SelectedPerson != null;
        }

        private void DeletePersonCommandExecute()
        {
            _dataPersons.Remove(SelectedPerson);
            RefreshPersons();
        }

        /// <summary>
        /// Проверка на возможность удалить запись.
        /// </summary>
        private bool DeletePersonCommandCanExecute()
        {
            // Если есть выбранная запись, то можно удалить.
            return SelectedPerson != null;
        }

        /// <summary>
        /// Обновить коллекцию данных из источника.
        /// </summary>
        private void RefreshPersons()
        {
            Persons = new ObservableCollection<Person>(_dataPersons.GetAll());
        }

        #endregion -- Command executers --
    }
}