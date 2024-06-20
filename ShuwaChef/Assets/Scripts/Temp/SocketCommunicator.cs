using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SocketCommunicator : MonoBehaviour
{


    [SerializeField] private string ip = "localhost"; 
    [SerializeField] private int port = 8808;


    private TcpClient client;
    private NetworkStream stream;
    public event EventHandler<OnSocketMessageReceiveArg> OnSocketMessageReceive;
    
    public class OnSocketMessageReceiveArg : EventArgs {
        public string message;
    }



    
    private async void Start()
    {
        await ConnectToServerAsync(ip,port);
        _ = ReceiveMessagesAsync(); // メッセージの非同期受信を開始
    }

    private async Task ConnectToServerAsync(string server, int port)
    {
        try
        {
            client = new TcpClient();
            await client.ConnectAsync(server, port);
            stream = client.GetStream();
            Debug.Log("Connected to server");
        }
        catch (Exception e)
        {
            Debug.LogError($"Socket error: {e}");
        }
    }

    private async Task ReceiveMessagesAsync()
    {
        try
        {
            byte[] receivedData = new byte[1024];
            while (true)
            {
                int bytesReceived = await stream.ReadAsync(receivedData, 0, receivedData.Length);
         
                if (bytesReceived == 0)
                {
                    Debug.Log("Server closed connection");
                    break;
                }
                string receivedMessage = Encoding.UTF8.GetString(receivedData, 0, bytesReceived);
                OnSocketMessageReceive?.Invoke(this, new OnSocketMessageReceiveArg { message = receivedMessage });
                Debug.Log($"Received from Python: {receivedMessage.Trim()}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Socket error: {e}");
        }
        finally
        {
            stream.Close();
            client.Close();
        }
    }
}
