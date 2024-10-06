using LunaSenatiConnection.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using LunaSenatiConnection.Services;
using Xamarin.Forms;

namespace LunaSenatiConnection.ViewModel
{
	public class MainPageViewModel : BaseViewModel

	{
        private ObservableCollection<Message> messages;
        private Message selectedChat; //SELECCION DE MENSAJE
        private readonly WebSocketService _webSocketService;

        public ObservableCollection<Message> Messages
        {
            get => messages;
            set => SetProperty(ref messages, value);
        }

        public Message SelectedChat
        {
            get => selectedChat;
            set
            {
                if (SetProperty(ref selectedChat, value) && selectedChat != null)
                {
                    // Ejecuta el comando de navegación
                    ChatSelectedCommand.Execute(selectedChat);
                }
            }
        }

        public Command<Message> ChatSelectedCommand { get; }

        public MainPageViewModel()
        {
            Messages = new ObservableCollection<Message>();
            GenerateSource();

            // Inicializa el servicio WebSocket con la URL del servidor
            _webSocketService = new WebSocketService("wss://senati-back-connection.centralus.cloudapp.azure.com");

            // Define el comando que se ejecutará al seleccionar un chat
            ChatSelectedCommand = new Command<Message>(OnNavigateToChat);

            // Conectar al WebSocket cuando se cree el ViewModel
            ConnectToWebSocket();

        }

        private async void ConnectToWebSocket()
        {
            try
            {
                await _webSocketService.ConnectAsync();
                Debug.WriteLine("Conexión establecida con WebSocket.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al conectar con WebSocket: {ex.Message}");
            }
        }


        private async void OnNavigateToChat(Message selectedChat)
        {
            if (selectedChat == null)
                return; // Evitar navegación si no hay chat seleccionado

            try
            {
                Debug.WriteLine($"NAVEGANDO EXITOSAMENTE CON: {selectedChat.Sender}, {selectedChat.Content}");
                await Application.Current.MainPage.Navigation.PushAsync(new Chat(selectedChat));
            }
            catch (Exception ex)
            {
                // Loguear el error o manejar la excepción
                Debug.WriteLine($"Error al navegar: {ex.Message}");
            }
        }



        // Mensajes de ejemplo
        private void GenerateSource()
        {
            Messages.Add(new Message { 
                Sender = "Desarrollo de Aplicaciones Móviles", 
                Content = "Hola!", 
                Initials = "AP" });
            Messages.Add(new Message { 
                Sender = "Seminario de Complementacion III", 
                Content = "¿Cómo estás?", 
                Initials = "SC" });
            Messages.Add(new Message { 
                Sender = "Mejora de Metodos", 
                Content = "Buenas noches",  
                Initials = "MM" });
        }
    }
    public class Message
    {
        public string Sender { get; set; }          // Titulo del chat
        public string Content { get; set; }         // Contenido del mensaje
        public string Initials { get; set; }        // Iniciales para el avatar
    }

}