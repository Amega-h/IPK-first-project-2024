using System.Text.RegularExpressions;
namespace IPK_client;

public class ResponseParser
{
    public void ParseTCP(string message,out string code,out string msg)
    {
        string recievedMessageTemp = message;
        string[] recievedMessage = message.Split();
        code = null;
        msg = null;
        switch (recievedMessage[0])
        {
            case "MSG":
                msg = RegexSplitMsg(recievedMessageTemp);
                Console.WriteLine($"{recievedMessage[2]}: {msg}");
                code = "MSG";
                break;
            case "ERR":
                msg = RegexSplitMsg(recievedMessageTemp);
                Console.Error.WriteLine($"ERR FROM {recievedMessage[2]}: {msg}");
                code = "ERR";
                break;
            case "BYE":
                code = "BYE";
                break;
            case "REPLY":
                if (recievedMessage[1] == "OK")
                {
                    msg = RegexSplitMsg(recievedMessageTemp);
                    Console.Error.WriteLine($"Success: {msg}");
                    code = "REPLY";
                    UserParse.isAuthed = true;
                    AuthSemaphore.SemaphoreAuth.Release();
                }
                else
                {
                    msg = RegexSplitMsg(recievedMessageTemp);
                    AuthSemaphore.SemaphoreAuth.Release();
                    code = "NOK";
                }
                break;
            default:
                code = "UNEXMSG";
                msg = message;
                break;
        }
        
    }

    // public void ParseUDP(string message)
    // {
    //     
    // }

    public string RegexSplitMsg(string message)
    {
        string regexTemplate = @" \bIS ";
        Regex regex = new Regex(regexTemplate);
        MatchCollection matches = regex.Matches(message);
        string[] messageContentArray  = message.Split(matches[0].ToString());
        return  messageContentArray[1];
    }
}