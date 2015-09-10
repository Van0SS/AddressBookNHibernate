using System.Windows;
using AddressBookClientApp.Messages;
using AddressBookClientApp.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace AddressBookClientApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private DialogView _dialogView;

        public MainView()
        {
            InitializeComponent();

            Closing += (s, e) => ViewModelLocator.Cleanup();
            Loaded += (s, e) =>
            {
                _dialogView = new DialogView(this);
            };

            Messenger.Default.Register<ShowDialogViewMessage>(this, (msg) => _dialogView.ShowDialog());
            Messenger.Default.Register<HideDialogViewMessage>(this, (msg) => _dialogView.Hide());
        }
    }
}
