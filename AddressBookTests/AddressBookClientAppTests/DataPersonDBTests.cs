using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressBookClientApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddressBookTests.AddressBookClientAppTests
{
    [TestClass]
    public class DataPersonDBTests
    {
        [TestMethod]
        public void Test()
        {
            DataPersonsDB dataPersons = new DataPersonsDB();
            var a = dataPersons.GetAll();

        }
    }
}
