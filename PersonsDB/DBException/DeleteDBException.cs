using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonsDB.DBException
{
    public class DeleteDBException : Exception
    {
        public DeleteDBException() { }

        public DeleteDBException(string message) : base(message) { }

        public DeleteDBException(string message, Exception inner)
        : base(message, inner)
        { }
    }
}
