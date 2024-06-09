
using System.Text;

namespace IPK_client;

public class Message
{
    //Class wich i would use, if i had enough time to implement udp
    public static int CONFIRMID = 0;
    public static int REPLYID = 1;
    public static int AUTHID = 2;
    public static int JOINID = 3;
    public static int MSGID = 4;
    public static int ERRID = 5;
    public static int BYEID = 6;
    public byte[]? encodedMsg = null;
    
    public byte[] EncodeUDP(string message,int idOfOperation,int counter)
    {
        byte[]? bytesToSend = null;
        if (idOfOperation == MSGID)
        {
            byte[]? bytesID = null;
            bytesID[0] = 0x04;
            //messageID logic add
            byte[]? bytesMessageId = BitConverter.GetBytes((ushort)counter);
            //displayName logic add
            bytesToSend = Encoding.ASCII.GetBytes(message);
            //byte[]? bytesDisplayName = Encoding.ASCII.GetBytes(args[2]);
        }
        return bytesToSend;
    }
    public byte[] EncodeUDP(string[] args,int idOfOperation,int counter)
    {
        byte[]? bytesToSend = null;
        if (idOfOperation == AUTHID)
        {
            byte[]? bytesID = null;
            bytesID[0] = 0x02;
            byte[]? bytesMessageId = BitConverter.GetBytes((ushort)counter);
            byte[]? bytesUsername = Encoding.ASCII.GetBytes(args[1]);
            byte[]? bytesDelimiterFirst = null;
            bytesDelimiterFirst[0] = 0x00;
            byte[]? bytesDisplayName = Encoding.ASCII.GetBytes(args[2]);
            byte[]? bytesDelimiterSecond = null;
            bytesDelimiterSecond[0] = 0x00;
            byte[]? bytesSecret = Encoding.ASCII.GetBytes(args[3]);
            byte[]? bytesDelimiterThird = null;
            bytesDelimiterThird[0] = 0x00;

            int index = 0;

            bytesToSend = new byte[bytesID.Length + bytesMessageId.Length + bytesUsername.Length +
                                   bytesDelimiterFirst.Length + bytesDisplayName.Length + bytesDelimiterSecond.Length +
                                   bytesSecret.Length + bytesDelimiterThird.Length];
            bytesID.CopyTo(bytesToSend,index);
            index += bytesID.Length;
            bytesMessageId.CopyTo(bytesToSend,index);
            index += bytesMessageId.Length;
            bytesUsername.CopyTo(bytesToSend, index);
            index += bytesUsername.Length;
            bytesDelimiterFirst.CopyTo(bytesToSend, index);
            index += bytesDelimiterFirst.Length;
            bytesDisplayName.CopyTo(bytesToSend, index);
            index += bytesDisplayName.Length;
            bytesDelimiterSecond.CopyTo(bytesToSend, index);
            index += bytesDelimiterSecond.Length;
            bytesSecret.CopyTo(bytesToSend, index);
            index += bytesSecret.Length;
            bytesDelimiterThird.CopyTo(bytesToSend, index);

        }
        return bytesToSend;
    }

    // public string DecodeUDP(byte[] bytes)
    // {
    //     
    // }
}