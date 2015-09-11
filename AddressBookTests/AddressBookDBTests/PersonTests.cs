using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressBookTests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonsDB.Domain;

namespace AddressBookTests.AddressBookDBTests
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void SetEmptyName_InvokeArgumentException()
        {
            MyAssert.Throws<ArgumentException>(() => new Person() {Name = "", Surname = "B"});
        }

        [TestMethod]
        public void SetEmptySurname_InvokeArgumentException()
        {
            MyAssert.Throws<ArgumentException>(() => new Person() {Name = "A", Surname = ""});
        }

        [TestMethod]
        public void SetWrongPhone_InvokeArgumentException()
        {
            MyAssert.Throws<ArgumentException>(() => new Person() {Name = "A", Surname = "B", Phone = "text"});

            MyAssert.Throws<ArgumentException>(() => new Person() {Name = "A", Surname = "B", Phone = "+7696644andtext"});

            MyAssert.Throws<ArgumentException>(() => new Person() {Name = "A", Surname = "B", Phone = "865+65"});

            MyAssert.Throws<ArgumentException>(() => new Person() {Name = "A", Surname = "B", Phone = "86565+"});

            MyAssert.Throws<ArgumentException>(() => new Person() {Name = "A", Surname = "B", Phone = "8655 56565"});

            MyAssert.Throws<ArgumentException>(() => new Person() {Name = "A", Surname = "B", Phone = "8-655-565-65"});

            // Больше чем 20 цифр
            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", Phone = "123456789012345678901" });
        }

        [TestMethod]
        public void SetRightPhone_NoExeptions()
        {
            new Person() {Name = "A", Surname = "B", Phone = "865556565"};

            new Person() {Name = "A", Surname = "B", Phone = "+865556565"};

            new Person() { Name = "A", Surname = "B", Phone = "" };
        }

        [TestMethod]
        public void SetWrongICQ_InvokeArgumentException()
        {
            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", ICQ = "text" });

            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", ICQ = "764ntext" });

            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", ICQ = "+86565" });

            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", ICQ = "8655 565" });

            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", ICQ = "8-655-56" });

            //Больше чем 9 цифр
            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", ICQ = "1234567890" });
        }

        [TestMethod]
        public void SetRightICQ_NoExeptions()
        {
            new Person() { Name = "A", Surname = "B", ICQ = "865556565" };

            new Person() { Name = "A", Surname = "B", ICQ = "" };
        }

        [TestMethod]
        public void SetWrongEmail_InvokeArgumentException()
        {
            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", Email = "text" });

            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", Email = "text@text" });

            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", Email = "собака@text.ru" });

            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", Email = "te..xt@text.tx" });

            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", Email = "-te.xt@text.tx" });
        }

        [TestMethod]
        public void SetRightEmail_NoExeptions()
        {
            new Person() { Name = "A", Surname = "B", Email = "te.8xt@text.tx" };

            new Person() { Name = "A", Surname = "B", Email = "" };
        }

        [TestMethod]
        public void SetWrongSkype_InvokeArgumentException()
        {
            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", Skype = "скайп" });

            MyAssert.Throws<ArgumentException>(() => new Person() { Name = "A", Surname = "B", Skype = "text@text" });
        }

        [TestMethod]
        public void SetRightSkype_NoExeptions()
        {
            new Person() { Name = "A", Surname = "B", Skype = "texth" };

            new Person() { Name = "A", Surname = "B", Skype = "text__h" };

            new Person() { Name = "A", Surname = "B", Skype = "" };
        }
    }
}
