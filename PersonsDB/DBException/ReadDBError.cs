using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonsDB.DBException
{
    public class ReadDBException : Exception
    {
        public ReadDBException() { }

        public ReadDBException(string message) : base(message) { }

        public ReadDBException(string message, Exception inner)
        : base(message, inner)
        { }
    }
}
