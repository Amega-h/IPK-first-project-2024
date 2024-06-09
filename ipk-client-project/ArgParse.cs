using System.Net;

namespace IPK_client;

public class ArgParse
{
    public Arguments parse(string[] args)
    {
        //new arguments instance
        Arguments arguments = new Arguments();
        
        //cycle which reads all the arguments you enter, when starting programm
        for (int i = 0; i < args.Length; i++)
        {
            
            //bool variable that shows if it is anny error in parsing arguments , if it is, throws error
            bool no_error = true;
            
            //decide , which actual argument do we have
            switch (args[i])
            {
                
                //Prints program help output and exits
                case "-h":
                 Console.WriteLine("-t|\tTransport protocol used for connection\n" +
                                   "-s|\tServer IP or hostname\n" +
                                   "-p|\tServer port\n" +
                                   "-d|\tUDP confirmation timeout\n" +
                                   "-r|\tMaximum number of UDP retransmissions\n"+
                                   "-h|\tView help");
                 break;
                
                //Maximum number of UDP retransmissions
                case "-r":
                    no_error = byte.TryParse(args[i+1],out arguments.MaxTrans);
                    if (no_error)
                        i++;
                    break;
                
                //UDP confirmation timeout
                case "-d":
                    no_error = ushort.TryParse(args[i+1],out arguments.UpdTimeout);
                    if (no_error)
                        i++;
                    break;
                case "-p":
                    no_error = ushort.TryParse(args[i+1],out arguments.ServerPort);
                    if (no_error)
                        i++;
                    break;
                case "-s":
                    if ((i + 1) >= args.Length)
                    {
                        no_error = false;
                        Console.Error.WriteLine("ERR: Not enough arguments after -s!\n");
                    }
                    arguments.ServerIP = args[i + 1];
                    if (args[i + 1] == "localhost")
                    {
                        arguments.ServerIP = "127.0.0.1";
                    }
                    else
                    {
                        IPAddress ip = IPAddress.TryParse(args[i+1], out _)
                            ? IPAddress.Parse(args[i+1])
                            : Dns.GetHostEntry(args[i+1]).AddressList[0].MapToIPv4();
                        arguments.ServerIP = ip.ToString();
                    }
                    i++;
                    break;
                case "-t":
                    if ((i + 1) >= args.Length)
                    {
                        no_error = false;
                        Console.Error.WriteLine("ERR: Not enough arguments after -t!\n");
                    }
                    if ((args[i + 1] == "tcp") || (args[i + 1] == "udp"))
                    {
                        arguments.TransportProtocol = args[i + 1];
                        i++;
                    }
                    else
                    {
                        no_error = false;
                        Console.Error.WriteLine("ERR: Wrong transport protocol format!\n");
                    }
                    break;
                default:
                    Console.Error.WriteLine("ERR: Some error occured in arguments!");
                break;
            }

            if (!no_error){
                Console.Error.WriteLine("ERR: Some error occured in arguments!\n");
                break;
            }
        }

        if (arguments.ServerIP == null)
        {
            Console.Error.WriteLine("ERR: Lack server`s IP !\n");
            Environment.Exit(1);
        }
        if (arguments.TransportProtocol == null)
        {
            Console.Error.WriteLine("ERR: Lack transport protocol !\n");
            Environment.Exit(1);
        }
        return arguments;
    }
}