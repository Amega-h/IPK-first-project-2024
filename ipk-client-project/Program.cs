
using System.Text;

namespace IPK_client;
class Program
{
    static async Task Main(string[] args)
    {
        ArgParse argParse = new ArgParse();
        Arguments arguments = argParse.parse(args);
        if (arguments.TransportProtocol == "udp")
        {
            Console.Error.WriteLine("Sorry! Udp protocol is not supported ! Author of the code noticed project too late!");
            Environment.Exit(0);
        }
        TCP tcp = new TCP();
        UDP udp = new UDP();
        UserParse userParse = new UserParse();
        AuthSemaphore.SemaphoreAuth = new SemaphoreSlim(0);
        if (arguments.TransportProtocol == "tcp")
        {
           await tcp.Connect(arguments);
        }
        else
        { 
           await udp.Connect(arguments);
        }
        
        while (true)
        {
            byte[]? bytesToSend = null;
            arguments.message = Console.ReadLine();
            string beginningStr = arguments.message;
            //if null was parced
            if (arguments.message == null)
            {
                await tcp.SendMessage(Encoding.ASCII.GetBytes("BYE\r\n"));
                await tcp.CloseStreams();
                Environment.Exit(0);
            } 
            //if it is a command , parse it and check for errors
            if (arguments.message[0] == '/')
            {
                bytesToSend = await userParse.ParseCmd(arguments);
                if (bytesToSend == null)
                {
                    Console.Error.WriteLine("ERR: Unknown command or wrong number of arguments!" +
                                      "Type /help to view available commands!");
                    continue;
                }
                if (bytesToSend[0] == 0x05)
                {
                    continue;
                }
                //if it`s not , just parse it
            } else {
                    bytesToSend = userParse.ParseMsg(arguments);
            }

            if (arguments.TransportProtocol == "tcp")
            {
                //if it`s not command , check authentication and send message or error
                if (beginningStr[0] != '/')
                {
                    if (UserParse.isAuthed)
                    {
                        await tcp.SendMessage(bytesToSend);
                    }
                    else
                    {
                        Console.Error.WriteLine("ERR: You must authenticate first!" +
                                                "Type /help to view available commands!");
                    }
                }
                //if it is , send it to server and if it is auth, wait for response
                else
                {
                    await tcp.SendMessage(bytesToSend);
                    if (beginningStr.Substring(0, 5) == "/auth")
                    {
                        await AuthSemaphore.SemaphoreAuth.WaitAsync();
                    }
                }
               
            }
            else
            {
                if (UserParse.isAuthed)
                {
                    //await udp.SendMessage(bytesToSend);
                }
                else
                {
                    Console.Error.WriteLine("ERR: You must authenticate first!" +
                                            "Type /help to view available commands!");
                }
            }
        }
    }
}