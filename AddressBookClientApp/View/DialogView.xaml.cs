﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AddressBookClientApp.Message;
using GalaSoft.MvvmLight.Messaging;

namespace AddressBookClientApp.View
{
    /// <summary>
    /// Interaction logic for DialogView.xaml
    /// </summary>
    public partial class DialogView : Window
    {
        public DialogView(Window owner)
        {
            InitializeComponent();

            // Запись главного окна.
            Owner = owner;

            // При попытке закрыть, скрывать форму.
            Closing += (s, e) =>
            {
                e.Cancel = true;
                Messenger.Default.Send(new HideDialogViewMessage());
            };
        }
    }
}
