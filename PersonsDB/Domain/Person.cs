using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonsDB.Domain
{
    public class Person : ICloneable
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        /// <summary>
        /// Псевдоним
        /// </summary>
        public virtual string Nickname { get; set; }

        /// <summary>
        /// Почтовый адрес
        /// </summary>
        public virtual string MailAddress { get; set; }

        public virtual string Email { get; set; }

        public virtual string Phone { get; set; }

        public virtual string ICQ { get; set; }

        public virtual string Skype { get; set; }


        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
