using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
// using System.Threading;
using System.Threading.Tasks;

public class UDPActionServer
{

    private UdpClient anUdpClient;

    private Action<string> aCallback;

    // private Thread aThread;
    
    private int aReceiverPort;

    public static UDPActionServer Start(int listenPort, int receiverPort, Action<string> callback)
    {
        UDPActionServer anUdpActionServer = new UDPActionServer();
        anUdpActionServer.StartReceive(listenPort, receiverPort, callback);
        return anUdpActionServer;
    }

    public void StartReceive(int listenPort, int receiverPort, Action<string> callback)
    {
        IPEndPoint anIpEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
        this.anUdpClient = new UdpClient(anIpEndPoint);
        this.aCallback = callback;
        this.aReceiverPort = receiverPort;
        this.anUdpClient.BeginReceive(new AsyncCallback(this.ReceiveCallback), null);
        // this.aThread = new Thread(new ThreadStart(this.ReceiveCallback));
        // this.aThread.Start();
    }

    public async void Stop()
    {
        // this.aThread.Abort();
        byte[] payload = Encoding.UTF8.GetBytes("stop");
        this.anUdpClient.Send(payload, payload.Length, new IPEndPoint(IPAddress.Loopback, this.aReceiverPort));
        await Task.Delay(1000);
        this.anUdpClient?.Close();
        this.anUdpClient?.Dispose();
    }

    // public void ReceiveCallback()
    // {
    //     while (true)
    //     {
    //         IPEndPoint anIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
    //         byte[] aData = this.anUdpClient.Receive(ref anIPEndPoint);
    //         string aMessage = System.Text.Encoding.UTF8.GetString(aData);
    //         UnityMainThread.Instance.AddJob(() => this.aCallback(aMessage));
    //     }
    // }

    public void ReceiveCallback(IAsyncResult anAsyncResult)
    {
        IPEndPoint anIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] aData = this.anUdpClient.EndReceive(anAsyncResult, ref anIPEndPoint);
        string aMessage = System.Text.Encoding.UTF8.GetString(aData);
        this.anUdpClient.BeginReceive(new AsyncCallback(this.ReceiveCallback), null);
        UnityMainThread.Instance.AddJob(() => this.aCallback(aMessage));
    }
    
}