using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocketIOClient;

namespace LunaSenatiConnection.Services
{
    public class WebSocketService
    {
        private SocketIO _socket;
        private readonly HttpClient _httpClient;
        public Action<ChatMessage> OnMessageReceived { get; set; }

        public WebSocketService(string url)
        {
            // Inicializa el socket con la URL del servidor
            _socket = new SocketIO(url);
            _httpClient = new HttpClient();
        }

        public async Task ConnectAsync()
        {
            await _socket.ConnectAsync();

            // Escucha el evento 'welcome'
            _socket.On("welcome", response =>
            {
                Console.WriteLine(response.GetValue<string>());
            });

            // Escucha el evento 'chat'
            _socket.On("chat", response =>
            {
                var data = response.GetValue<ChatMessage>();
                OnMessageReceived?.Invoke(data);
                Console.WriteLine($"Mensaje de {data.FromUser}: {data.Message}");
            });
        }

        public async Task JoinCourseAsync(string idUser, string idCurso)
        {
            await _socket.EmitAsync("join", new { idUser, idCurso });
            Console.WriteLine($"Unido al curso {idCurso} como usuario {idUser}");
        }

        //public async Task SendMessageAsync(string idCurso, string fromUser, string initials, string message, string align)
        //{
        //    // Envía un mensaje al servidor
        //    await _socket.EmitAsync("chat", new { idCurso, fromUser, initials, message, align });
        //}

        public async Task SendMessageAsync(string idCurso, string message)
        {
            var url = "https://senati-back-connection.centralus.cloudapp.azure.com/api/chat";

            // Crear el objeto a enviar
            var messageData = new
            {
                idCurso,
                mensaje = message
            };

            // Serializar el objeto a JSON
            var jsonContent = new StringContent(JsonConvert.SerializeObject(messageData), Encoding.UTF8, "application/json");

            try
            {
                // Realizar la solicitud POST
                var response = await _httpClient.PostAsync(url, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Mensaje enviado exitosamente: {responseContent}");
                }
                else
                {
                    Console.WriteLine($"Error al enviar el mensaje: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al enviar el mensaje: {ex.Message}");
            }
        }

        public async Task DisconnectAsync()
        {
            await _socket.DisconnectAsync();
        }
    }

    public class ChatMessage
    {
        public string FromUser { get; set; }
        public string Initials { get; set; }
        public string Message { get; set; }
        public string Align { get; set; }
    }
}