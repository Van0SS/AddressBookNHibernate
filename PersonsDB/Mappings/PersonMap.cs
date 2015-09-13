using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using PersonsDB.Domain;

namespace PersonsDB.Mappings
{
    /// <summary>
    /// Маппинг для <see cref="Person"/> через Fluent NHibernate.
    /// </summary>
    class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        { 
            Id(x => x.Id);

            Map(x => x.Name)
                .Not.Nullable(); // Обязательное поле.

            Map(x => x.Surname)
                .Not.Nullable(); // Обязательное поле.

            Map(x => x.Nickname);

            Map(x => x.MailAddress);

            Map(x => x.Email);

            Map(x => x.Phone).Length(20); // Максимальная длина номер 20 (на всякий).

            Map(x => x.ICQ).Length(9); // Вряд ли мэйлру уже больше сделает.

            Map(x => x.Skype);
        }
    }
}
