using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddressBookTests.TestHelpers
{
    public static class MyAssert
    {
        /// <summary>
        /// Проверить Функцию на выброс исключения.
        /// </summary>
        /// <typeparam name="T">Тип исключения</typeparam>
        /// <param name="func">Проверяемая функция</param>
        public static void Throws<T>(Action func) where T : Exception
        {
            var exceptionThrown = false;
            try
            {
                func.Invoke();
            }
            catch (T)
            {
                // Был вызван нужный тип исключения.
                exceptionThrown = true;
            }

            if (!exceptionThrown)
            {
                // Не был вызван нужный тип исключения.
                throw new AssertFailedException(
                    String.Format("An exception of type {0} was expected, but not thrown", typeof(T))
                    );
            }
        }
    }
}
