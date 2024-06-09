using System.Net.Sockets;
using System.Text;

namespace IPK_client;

public class TCP
{
    private TcpClient client;
    private StreamWriter _streamWriter;
    private StreamReader _streamReader;
    public async Task Connect(Arguments arguments)
    {
        string serverIP = arguments.ServerIP;
        ushort serverPort = arguments.ServerPort;

        try
        {
            client = new TcpClient(serverIP, serverPort);
            _streamReader = new StreamReader(client.GetStream());
            _streamWriter = new StreamWriter(client.GetStream());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Task.Run(RecieveMessage);
    }

    public async Task SendMessage(byte[] bytesToSend)
    {
        await _streamWriter.WriteAsync(Encoding.ASCII.GetString(bytesToSend));
        await _streamWriter.FlushAsync();
    }
    public async Task RecieveMessage()
    {
        ResponseParser responseParser = new ResponseParser();
        while (true)
        {
            string? recievedMessage = await _streamReader.ReadLineAsync();
            string? code = null;
            string? msg = null;
            responseParser.ParseTCP(recievedMessage,out code,out msg);
            if (code == "BYE")
            {
                await SendMessage(Encoding.ASCII.GetBytes("BYE\r\n"));
                await CloseStreams();
                Environment.Exit(0);
            }
            if (code == "ERR")
            {
                await SendMessage(Encoding.ASCII.GetBytes("BYE\r\n"));
            }
            if (code == "NOK")
            {
                Console.Error.WriteLine($"Failure: {msg}");
            }
            if (code == "UNEXMSG")
            {
                await SendMessage(Encoding.ASCII.GetBytes($"ERR FROM {UserParse.displayName} IS {msg}\r\n"));
                Console.Error.WriteLine($"ERR: {msg}. Unexpected error from server!");
            }
        }
    }

    public async Task CloseStreams()
    {
        _streamWriter.Close();
        _streamWriter.Close();
    }
}