using System.Text;

namespace IPK_client;

public class UserParse
{
    public int messageID = 0;
    public static string? displayName = null;
    public static bool isAuthed = false;
    public async Task<byte[]?> ParseCmd(Arguments arguments)
    {
        Message message = new Message();
        byte[]? doneMsg = null;
            arguments.message = arguments.message.Remove(0, 1);
            string[] splittedString = arguments.message.Split();
            if (splittedString.Length != 4 && splittedString.Length != 2 && splittedString.Length != 1)
            {
                return null;
            }
            if (splittedString[0].Length == 4)
            {
                switch (splittedString[0])
                {
                    case "auth":
                        if (splittedString.Length != 4)
                        {
                            return null;
                        }
                        if (arguments.TransportProtocol == "tcp")
                        {
                            if (isAuthed) return null;
                            doneMsg = Encoding.ASCII.GetBytes(
                                $"AUTH {splittedString[1]} AS {splittedString[3]} USING {splittedString[2]}\r\n");
                            displayName = splittedString[3];
                        }
                        else
                        {
                            doneMsg = message.EncodeUDP(splittedString, Message.AUTHID,messageID);
                            displayName = splittedString[3];
                            messageID++;
                        }
                        break;
                    case "join":
                        if (splittedString.Length != 2)
                        {
                            return null;
                        }
                        if (arguments.TransportProtocol == "tcp")
                        {
                            doneMsg = Encoding.ASCII.GetBytes(
                                $"JOIN {splittedString[1]} AS {displayName}\r\n");
                        }
                        else
                        {
                            //add logic
                        }
                        break;
                    case "help":
                        if (splittedString.Length != 1)
                        {
                            return null;
                        }
                        Console.WriteLine("/auth {Username} {Secret} {DisplayName} ---\tSends AUTH message with the data provided from the command to the server (and correctly handles the Reply message), locally sets the DisplayName value (same as the /rename command)\n");
                        Console.WriteLine("/join {ChannelID}                       ---\tSends JOIN message with channel name from the command to the server (and correctly handles the Reply message)\n");
                        Console.WriteLine("/rename {DisplayName}                   ---\tLocally changes the display name of the user to be sent with new messages/selected commands\n");
                        Console.WriteLine("/help                                   ---\tPrints out supported local commands with their parameters and a description\n");
                        return new Byte[] { 0x05 };
                        break;
                    default:
                        return null;
                }
            } else if (splittedString[0].Length == 6)
            {
                if (splittedString[0] == "rename")
                {
                    if (splittedString.Length != 2)
                    {
                        return null;
                    }
                    displayName = splittedString[1];
                    return new Byte[] { 0x05 };
                }
                else 
                    return null;
            } else 
                return null;
        
        return doneMsg;
    }

    public byte[] ParseMsg(Arguments arguments)
    {
        byte[]? doneMsg = null;
        Message message = new Message();
        if (arguments.TransportProtocol == "tcp")
        {
            doneMsg = Encoding.ASCII.GetBytes(
                $"MSG FROM {displayName} IS {arguments.message}\r\n");
        }
        else
        {
            //add logic
        }

        return doneMsg;
    }
}