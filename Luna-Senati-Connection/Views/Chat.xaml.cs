using LunaSenatiConnection.ViewModel;
using LunaSenatiConnection.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LunaSenatiConnection.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chat : ContentPage
    {
        private readonly WebSocketService _webSocketService;
        public Chat(Message selectedMessage)
        {
            InitializeComponent();
            _webSocketService = new WebSocketService("wss://senati-back-connection.centralus.cloudapp.azure.com");
            BindingContext = new ChatViewModel(selectedMessage, _webSocketService); // Establece el contexto de enlace
        }


    }
}