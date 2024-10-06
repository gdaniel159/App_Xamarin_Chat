using LunaSenatiConnection.Views;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LunaSenatiConnection.Services;
using System.Threading.Tasks;

namespace LunaSenatiConnection.ViewModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class ChatViewModel : BaseViewModel
    {
        private readonly WebSocketService _webSocketService;
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }
        public string Status { get; set; }

        private string newMessageContent;
        public string NewMessageContent
        {
            get => newMessageContent;
            set => SetProperty(ref newMessageContent, value);
        }

        private ObservableCollection<ChatViewModel> _messages;
        public ObservableCollection<ChatViewModel> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }

        public Command SendMessageCommand { get; }

        // Constructor que toma un chat seleccionado
        //public ChatViewModel(Message selectedMessage, WebSocketService webSocketService)
        //{
        //    FromUser = selectedMessage.Sender;
        //    _webSocketService = webSocketService;

        //    // Inicializa la colección de mensajes
        //    Messages = new ObservableCollection<ChatViewModel>{};
        //    SendMessageCommand = new Command(async () => await OnSendMessageAsync());

        //    _ = ConnectWebSocket();

        //    // Depuración: Imprimir mensajes en la colección
        //    foreach (var message in Messages)
        //    {
        //        Debug.WriteLine(
        //            $"From: {message.FromUser}, " +
        //            $"Message: {message.Message}, " +
        //            $"Status: {message.Status}");
        //    }
        //}

        public ChatViewModel(Message selectedMessage, WebSocketService webSocketService)
        {
            FromUser = selectedMessage.Sender;
            ToUser = "OtroUsuario"; // Define a quién estás enviando el mensaje
            _webSocketService = webSocketService;

            Messages = new ObservableCollection<ChatViewModel> { 
                
            };
            SendMessageCommand = new Command(async () => await OnSendMessageAsync());

            // Conectar el WebSocket
            _ = _webSocketService.ConnectAsync();
        }

        // Constructor adicional para mensajes de prueba
        public ChatViewModel(string fromUser, string toUser, string message, DateTime dateSent, string status)
        {
            FromUser = fromUser;
            ToUser = toUser;
            Message = message;
            DateSent = dateSent;
            Status = status;
        }

        private async Task OnSendMessageAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewMessageContent))
            {
                var newMessage = new ChatViewModel(
                    "Tú",  // Desde el usuario actual
                    ToUser,
                    NewMessageContent,
                    DateTime.Now,
                    "Sent");

                Messages.Add(newMessage);

                try
                {
                    await _webSocketService.SendMessageAsync("curso123", NewMessageContent);
                    Debug.WriteLine("Mensaje enviado exitosamente.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error al enviar el mensaje: {ex.Message}");
                }

                NewMessageContent = string.Empty;
                OnPropertyChanged(nameof(NewMessageContent));
            }
        }

        // Manejar el mensaje recibido
        private void OnMessageReceived(ChatMessage chatMessage)
        {
            // Agregar el mensaje recibido a la colección
            var receivedMessage = new ChatViewModel(
                chatMessage.FromUser,
                ToUser,
                chatMessage.Message,
                DateTime.Now,
                "Received");

            Messages.Add(receivedMessage);
        }

        public void Dispose()
        {
            // Desuscribirse del evento para evitar fugas de memoria
            if (_webSocketService != null)
            {
                _webSocketService.OnMessageReceived -= OnMessageReceived;
            }
        }

    }
}
