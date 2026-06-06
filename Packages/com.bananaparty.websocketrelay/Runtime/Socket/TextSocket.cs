using System.Text;

namespace BananaParty.WebSocketRelay
{
    /// <summary>
    /// Adapter for <see cref="Socket"/> that provides a semantic wrapper for text-based communication.
    /// </summary>
    public class TextSocket
    {
        private readonly Socket _socket;

        public TextSocket(string serverAddress)
        {
            _socket = new Socket(serverAddress);
        }

        public bool IsConnected => _socket.IsConnected;

        public bool HasUnreadPayloadQueue => _socket.HasUnreadPayloadQueue;

        public string ReadPayloadQueue() => Encoding.UTF8.GetString(_socket.ReadPayloadQueue());

        public void Connect() => _socket.Connect();

        public void Send(string text) => _socket.Send(Encoding.UTF8.GetBytes(text));

        public void Disconnect() => _socket.Disconnect();

        public void Dispose() => _socket.Dispose();
    }
}
