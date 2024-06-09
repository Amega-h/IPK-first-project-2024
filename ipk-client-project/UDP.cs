using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IPK_client;

public class UDP
{
    private UdpClient client;
    public async Task Connect(Arguments arguments)
    {
        string serverIP = arguments.ServerIP;
        ushort serverPort = arguments.ServerPort;

        try
        {
            using (UdpClient udpClient = new UdpClient())
            {
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                client = new UdpClient(serverEndPoint);

                // byte[] bytesToSend = Encoding.ASCII.GetBytes(messageToSend);
                //
                // udpClient.Send(bytesToSend, bytesToSend.Length, serverEndPoint);
                //
                // Console.WriteLine("Message sent.");
                //
                // IPEndPoint receiveEndPoint = new IPEndPoint(IPAddress.Any, 0);
                // byte[] receivedBytes = udpClient.Receive(ref receiveEndPoint);
                // string receivedMessage = Encoding.ASCII.GetString(receivedBytes);
                //
                // Console.WriteLine($"Server response ({receiveEndPoint}): {receivedMessage}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public async Task SendMessage(byte[] bytesToSend)
    {
        await client.SendAsync(bytesToSend,bytesToSend.Length);
    }
    public async Task RecieveMessage()
    {
        ResponseParser responseParser = new ResponseParser();
        while (true)
        {
            Message message = new Message();
            UdpReceiveResult receiveResult = await client.ReceiveAsync();
            byte[] buffer = receiveResult.Buffer;
            //string? recievedMessage = message.DecodeUDP(buffer);
            // string? recievedMessage = await _streamReader.ReadLineAsync();
            // string? code = null;
            // string? msg = null;
            // responseParser.ParseTCP(recievedMessage,out code,out msg);
            // if (code == "BYE")
            // {
            //     await SendMessage(Encoding.ASCII.GetBytes("BYE\r\n"));
            //     await CloseStreams();
            //     Environment.Exit(0);
            // }
            // if (code == "ERR")
            // {
            //     await SendMessage(Encoding.ASCII.GetBytes("BYE\r\n"));
            // }
            // if (code == "NOK")
            // {
            //     Console.Error.WriteLine($"Failure: {msg}");
            // }
            // if (code == "UNEXMSG")
            // {
            //     await SendMessage(Encoding.ASCII.GetBytes($"ERR FROM {UserParse.displayName} IS {msg}\r\n"));
            //     Console.Error.WriteLine($"ERR: {msg}. Unexpected error from server!");
            // }
        }
    }
}