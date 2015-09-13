using System.Windows;
using AddressBookClientApp.Message;
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

            // После загрузки главного окна, загрузить вспомогательное (Для редактирования).
            Loaded += (s, e) =>
            {
                _dialogView = new DialogView(this);
            };

            // Зарегистрировать события показать вспомогательную форму и скрыть её.
            Messenger.Default.Register<ShowDialogViewMessage>(this, (msg) => _dialogView.ShowDialog());
            Messenger.Default.Register<HideDialogViewMessage>(this, (msg) => _dialogView.Hide());
        }
    }
}
