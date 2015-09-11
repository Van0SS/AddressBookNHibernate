using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PersonsDB.Domain
{
    public class Person : ICloneable
    {
        public virtual Guid Id { get; set; }

        private string _name;

        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw  new ArgumentException("Name must be not empty");
                _name = value;
            }
        }

        private string _surname;

        public virtual string Surname
        {
            get { return _surname; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Surname must be not empty");
                _surname = value;
            }
        }

        /// <summary>
        /// Псевдоним
        /// </summary>
        public virtual string Nickname { get; set; }

        /// <summary>
        /// Почтовый адрес
        /// </summary>
        public virtual string MailAddress { get; set; }

        private string _email;

        public virtual string Email {
            get { return _email; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    // http://emailregex.com/ // Заменил \w на [A-Za-z_\d] чтобы имя было только латинскими буквами.
                    if (!Regex.IsMatch(value, @"^[A-Za-z_\d]+([-+.'][A-Za-z_\d]+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                        throw new ArgumentException("Value not have email view");
                }

                _email = value;
            }
        }

        private string _phone;
        public virtual string Phone {
            get { return _phone; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (!Regex.IsMatch(value, @"^\+?\d*$"))
                        throw new ArgumentException("Phone must contain only digits, or '+' at begin");

                    if (value.Length > 20)
                        throw new ArgumentException("Phone must be shorter than 21 digits");
                }

                _phone = value;
            }
        }

        private string _icq;

        public virtual string ICQ {
            get { return _icq; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (!Regex.IsMatch(value, @"^\d*$"))
                        throw new ArgumentException("ICQ must contain only digits");

                    if (value.Length > 9)
                        throw new ArgumentException("ICQ must be shorter than 10 digits");
                }
                _icq = value;
            }
        }

        private string _skype;
        public virtual string Skype {
            get { return _skype; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (!Regex.IsMatch(value, @"^[A-Za-z_\.]*$"))
                        throw new ArgumentException("Skype must contain only latin letters and '.' or '_'.");
                }

                _skype = value;
            }
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
