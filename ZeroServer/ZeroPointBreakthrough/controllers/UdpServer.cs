using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
public class UdpServer
{
    private bool isRunning;
    private GameController gc;
    private List<EndPoint> clientList = new List<EndPoint>();
    private Socket serverSocket = null;
    public UdpServer()
    {
         //create a new server
        var server = new UdpListener();
        string host = "127.0.0.1";
        int portToConnect = 32123;
        this.isRunning = true;
        this.gc = new GameController();
        
        initGameState();

        //start listening for messages and copy the messages back to the client
        Task.Factory.StartNew(async () => {
            while (true)
            {
                var received = await server.Receive();
                Console.WriteLine("Got UDP Packet from :" + received.Sender.ToString());
                Console.WriteLine("---------------------------------------");
                bool isCommand = false;
                
                if (received.Message.Contains("setPlayers")){
                    server.Reply("Players have been set!", received.Sender);
                    isCommand = true;
                }

                if (received.Message.Contains("getState")){
                    //GameState curState = GameController.Instance.getState();
                    //string message = curState.getMap();
                    //message = ":" + message + ":" + curState.getPlayerCount() + ":";
                    server.Reply("State-", received.Sender);
                    isCommand = true;
                }
                
                if (!isCommand){
                    server.Reply("Did not identify your command! Your command was:" + received.Message, received.Sender);
                }
            }
        });

        //create a new client
        var client = UdpUser.ConnectTo(host, portToConnect);

        //wait for reply messages from server and send them to console 
        Task.Factory.StartNew(async () => {
            while (true)
            {
                try
                {
                    var received = await client.Receive();
                    Console.WriteLine(received.Message);
                    if (received.Message.Contains("quit"))
                        break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        });

        //type ahead :-)
        string read;
        do
        {
            read = Console.ReadLine();
            client.Send(read,host,portToConnect);
        } while (read != "quit");
        
    }

    private void initGameState(){
        Console.WriteLine("---------------------------------------");
        Console.WriteLine("---------GameStateInitialized----------");
        Console.WriteLine("---------------------------------------");
    }

    public bool IsRunning()
    {
        return this.isRunning;
    }
}

public struct Received
{
    public IPEndPoint Sender;
    public string Message;
}

abstract class UdpBase
{
    protected UdpClient Client;

    protected UdpBase()
    {
        Client = new UdpClient();
    }

    public async Task<Received> Receive()
    {
        var result = await Client.ReceiveAsync();
        return new Received()
        {
            Message = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length),
            Sender = result.RemoteEndPoint
        };
    }
}

//Server
class UdpListener : UdpBase
{
    private IPEndPoint _listenOn;

    public UdpListener() : this(new IPEndPoint(IPAddress.Any,32123))
    {
    }

    public UdpListener(IPEndPoint endpoint)
    {
        _listenOn = endpoint;
        Client = new UdpClient(_listenOn);
    }

    public void Reply(string message,IPEndPoint endpoint)
    {
        var datagram = Encoding.ASCII.GetBytes(message);
        Client.SendAsync(datagram, datagram.Length,endpoint);
    }

}

//Client
class UdpUser : UdpBase
{
    private UdpUser(){}

    public static UdpUser ConnectTo(string hostname, int port)
    {
        SocketAsyncEventArgs e = new SocketAsyncEventArgs();
        e.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(hostname), port);
        var connection = new UdpUser();
        connection.Client.Client.ConnectAsync(e);//    Connect(hostname, port);
        return connection;
    }

    public void Send(string message,string hostname, int port)
    {
        var datagram = Encoding.ASCII.GetBytes(message);
        Client.SendAsync(datagram, datagram.Length,hostname,port);
    }

}