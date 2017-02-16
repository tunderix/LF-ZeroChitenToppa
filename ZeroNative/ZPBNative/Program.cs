using System;
using System.Text;
using System.Net.Sockets;

class ZPBNative
{
    static void Main(string[] args)
    {
        // Create UDP client
        int receiverPort = 32123;
        UdpClient receiver = new UdpClient(receiverPort);

        // Display some information
        Console.WriteLine("Sending Messages to localhost on port :" + receiverPort.ToString());
        Console.WriteLine("-------------------------------\n");
        Console.WriteLine("Press any key to quit.");
        Console.WriteLine("-------------------------------\n");

        // Send some test messages
        using (UdpClient sender1 = new UdpClient(19999))
            sender1.SendAsync(Encoding.ASCII.GetBytes("Hi!"), 3, "localhost", receiverPort);

        // Wait for any key to terminate application
        Console.ReadKey();
    }

    private void sendDataToServer(string hostname, int port)
    {
        UdpClient udpClient = new UdpClient();
        Byte[] senddata = Encoding.ASCII.GetBytes("Hello World");
        udpClient.SendAsync(senddata, senddata.Length, hostname, port);
    }
}
