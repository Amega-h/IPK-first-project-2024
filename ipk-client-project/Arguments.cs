namespace IPK_client;

public class Arguments
{
    public byte MaxTrans = 3;
    public ushort UpdTimeout = 250;
    public ushort ServerPort = 4567;
    public string? ServerIP;
    public string? TransportProtocol;
    public string? message;

    public void print()
    {
        Console.WriteLine("MaxTrans = " + MaxTrans.ToString() + "\n");
        Console.WriteLine("UpdTimeout = " + UpdTimeout.ToString() + "\n");
        Console.WriteLine("ServerPort = " + ServerPort.ToString() + "\n");
        Console.WriteLine("ServerIP = " + ServerIP + "\n");
        Console.WriteLine("TransportProtocol = " + TransportProtocol + "\n");
    }
}