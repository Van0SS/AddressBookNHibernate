using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using AddressBookClientApp.Message;
using AddressBookClientApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PersonsDB.Domain;

namespace AddressBookClientApp.ViewModel
{
    /// <summary>
    /// Окно для редактирования и добавления записей.
    /// </summary>
    public class DialogViewModel : ViewModelBase
    {

        #region -- Bindable changeable properties --

        /// <summary>
        /// The <see cref="AcceptingText" /> property's name.
        /// </summary>
        public const string AcceptingTextPropertyName = "AcceptingText";

        private string _acceptingText = "Add";

        /// <summary>
        /// Текст на кнопке принять изменения.
        /// </summary>
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

        /// <summary>
        /// Текущие данные для редактирования или добавления.
        /// </summary>
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

        #endregion -- Bindable changeable properties-- 

        #region -- Commands --

        /// <summary>
        /// Принять изменения.
        /// </summary>
        public ICommand AcceptChangesCommand { get; private set; }

        /// <summary>
        /// Отменить изменения.
        /// </summary>
        public ICommand CancelCommand { get; private set; }

        #endregion -- Commands --

        #region -- Constructor --

        public DialogViewModel()
        {
            // Регистрация сообщений для создания данных.
            Messenger.Default.Register<RequestAddPersonMessage>(this, RequestAddPersonRecieve);
            // Регистрация сообщений для редактированния данных.
            Messenger.Default.Register<SendPersonToEditMessage>(this, PersonToEditRecieve);

            AcceptChangesCommand = new RelayCommand(AcceptChangesCommandExecute, AcceptChangesCommandCanExecute);

            // При отмене, скрыть формы. Т.е. данные не отправятся.
            CancelCommand = new RelayCommand(() => Messenger.Default.Send(new HideDialogViewMessage()));
        }

        #endregion -- Constructor --

        #region -- Messege receivers --

        private void PersonToEditRecieve(SendPersonToEditMessage message)
        {
            // Если надо поменять данные, то кнопка принять называется - Edit
            AcceptingText = "Edit";

            // Создать копию данных, для того чтобы сразу не изменять данные, а только после принятия изменений.
            EditPerson = (Person) message.Person.Clone();
        }

        private void RequestAddPersonRecieve(RequestAddPersonMessage message)
        {
            // Если надо добавить запись, то кнопка принять называется - Add
            AcceptingText = "Add";

            // Создаём пустые данные для их заполнения.
            EditPerson = new Person();
        }

        #endregion -- Message receivers --

        #region -- Command executers --

        private void AcceptChangesCommandExecute()
        {
            // Проверка текущего режима (Добавление или редактирование)
            if (AcceptingText == "Add")
            {
                // Отправить новую запись.
                Messenger.Default.Send(new PersonToAddMessage(EditPerson));
            }
            else
            {
                // Отправить отредактированную запись.
                Messenger.Default.Send(new EditedPersonMessage(EditPerson));
            }

            // После всех операций скрыть форму редактирования.
            Messenger.Default.Send(new HideDialogViewMessage());
        }

        private bool AcceptChangesCommandCanExecute()
        {
            if (EditPerson == null)
                return false;

            // Если обязательные поля (Name, surname) заполенены то можно принимать изменения.
            return ((EditPerson.Name != null) && (EditPerson.Surname != null));
        }

        #endregion -- Command executers --

    }
}
