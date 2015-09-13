using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using PersonsDB.DBException;

namespace AddressBookClientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is ReadDBException)
            {
                e.Handled = true;
                MessageBox.Show("Error: " + e.Exception.Message);
            }
#if !DEBUG
            else
            {

                MessageBox.Show("Fatal error, application will be closed.");
                Current.Shutdown();

            }
#endif
        }
    }
}
