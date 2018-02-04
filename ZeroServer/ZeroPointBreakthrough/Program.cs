using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;

class ZeroPointBreakthrough
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting server");
        Run().Wait();
    }

    private static async Task Run()
    {
        var server = new UdpServer();

        while (server.IsRunning())
        {
            await Task.Delay(1);
        }
    }
}


