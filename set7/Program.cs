using NetMQ;
using NetMQ.Sockets;

public interface IMessageSource
{
    void SendMessage(string message);
    string ReceiveMessage();
}

public interface IMessageSourceClient
{
    void Connect();
    void SendMessage(string message);
    string ReceiveMessage();
    void Disconnect();
}

public class NetMQMessageSource : IMessageSource, IMessageSourceClient
{
    private readonly string _ipAddress;
    private readonly int _port;
    private PairSocket _socket;

    public NetMQMessageSource(string ipAddress, int port)
    {
        _ipAddress = ipAddress;
        _port = port;
    }

    public void SendMessage(string message)
    {
        _socket.SendFrame(message);
    }

    public string ReceiveMessage()
    {
        return _socket.ReceiveFrameString();
    }

    public void Connect()
    {
        _socket = new PairSocket();
        _socket.Connect($"tcp://{_ipAddress}:{_port}");
    }

    public void Disconnect()
    {
        _socket.Dispose();
    }
}