using System.ComponentModel;
using System.Security.Permissions;
using AddressBookClientApp.Message;
using AddressBookClientApp.Model;
using AddressBookClientApp.ViewModel;
using AddressBookTests.TestData;
using FluentAssertions;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersonsDB.Domain;

namespace AddressBookTests.AddressBookClientAppTests.ViewModel
{
    [TestClass]
    public class MainViewModelTests
    {
        private static Mock<IDataPersons> _mockDataPersons;

        private MainViewModel _mainViewModel;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockDataPersons = new Mock<IDataPersons>();
            _mockDataPersons.Setup(x => x.GetAll()).Returns((() => PersonsTestData.ValidPersons));
            _mainViewModel = new MainViewModel(_mockDataPersons.Object);
        }

        [TestMethod]
        public void Initilization_DataPersonsInvokeGetAll()
        {
            _mockDataPersons.Verify(x => x.GetAll());
        }

        [TestMethod]
        public void Initilization_PersonsTakenFromDataService()
        {
            CollectionAssert.AreEqual(PersonsTestData.ValidPersons, _mainViewModel.Persons);
        }

        [TestMethod]
        public void PersonIsSelected_PropertyChangedShouldBeRaised()
        {
            _mainViewModel.MonitorEvents();

            _mainViewModel.SelectedPerson = PersonsTestData.ValidPersons[0];

            _mainViewModel.ShouldRaise("PropertyChanged")
                .WithSender(_mainViewModel)
                .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == "SelectedPerson");
        }

        [TestMethod]
        public void DeletePersonCommandExecute_DataPersonsInvokeRemoveAtOnceTimes()
        {
            _mainViewModel.SelectedPerson = PersonsTestData.ValidPersons[0];
            _mainViewModel.DeletePersonCommand.Execute(null);
            _mockDataPersons.Verify(x => x.Remove(PersonsTestData.ValidPersons[0]), Times.Once);
        }

        [TestMethod]
        public void SendEditedPerson_EditedPersonMessageReceived_DataPersonsInvokeUpdate()
        {
            Messenger.Default.Send(new EditedPersonMessage(It.IsAny<Person>()));
            _mockDataPersons.Verify(x => x.Update(It.IsAny<Person>()), Times.Once);
        }

        [TestMethod]
        public void SendPersonToAdd_PersonToAddMessageReceive_DataPersonsInvokeAdd()
        {
            Messenger.Default.Send(new PersonToAddMessage(It.IsAny<Person>()));
            _mockDataPersons.Verify(x => x.Add(It.IsAny<Person>()), Times.Once);
        }
    }
}
