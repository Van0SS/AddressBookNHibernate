using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressBookClientApp.Model;
using AddressBookClientApp.ViewModel;
using AddressBookTests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersonsDB.Domain;

namespace AddressBookTests.AddressBookClientAppTests.ViewModel
{
    /// <summary>
    /// Тесты использующие несколько реальных сущностей.
    /// </summary>
    [TestClass]
    public class ComplexTests
    {
        private MainViewModel _mainViewModel;
        private DialogViewModel _dialogViewModel;

        private static Mock<IDataPersons> _mockDataPersons = new Mock<IDataPersons>();

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            _mockDataPersons.Setup(x => x.GetAll()).Returns((() => PersonsTestData.ValidPersons));
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _mainViewModel = new MainViewModel(_mockDataPersons.Object);
            _dialogViewModel = new DialogViewModel();
        }

        [TestMethod]
        public void MainVMSendAddRequestAtDialogVM_RequestAccepeted()
        {
            _mainViewModel.AddPersonCommand.Execute(null);

            Assert.IsNotNull(_dialogViewModel.EditPerson);
        }

        [TestMethod]
        public void MainVMSendEditRequestAtDialogVM_RequestAccepeted()
        {
            _mainViewModel.SelectedPerson = new Person() { Name = "Test" };
            _mainViewModel.EditPersonCommand.Execute(null);

            Assert.AreEqual("Test", _dialogViewModel.EditPerson.Name);
        }
    }
}
