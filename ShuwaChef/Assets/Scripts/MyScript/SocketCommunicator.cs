using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class SocketCommunicator : MonoBehaviour
{


    [SerializeField] private string ip = "localhost"; 
    [SerializeField] private int port = 8808;


    private TcpClient client;
    private NetworkStream stream;

    public event EventHandler OnSocketConnected;
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
    int attempts = 0;
    while (true)
    {
        try
        {
            
                client = new TcpClient();
                await client.ConnectAsync(server, port);
                stream = client.GetStream();
                Debug.Log("Connected to server");
                return; // 成功した場合、メソッドを終了
    
        }
        catch (Exception e)
        {
            Debug.LogError($"Unexpected error: {e.Message}");
        }

        attempts++;
    
            Debug.Log($"Retrying connection... Attempt {attempts + 1}");
            await Task.Delay(1500); // 次の試行まで待機
  
    }

    Debug.LogError("All connection attempts failed.");
     
    }


    public bool GetIsConnectedToServer() 
    {
        return client != null && client.Connected;
    }
    public async void RecordRequest() 
    {
        await SendMessageAsync("record");
    }



    public async Task SendMessageAsync(string message)
    {
        if (client == null || stream == null)
        {
            Debug.LogError("Client or stream is null");
            return;
        }
        byte[] data = Encoding.UTF8.GetBytes(message);
        await stream.WriteAsync(data, 0, data.Length);
        Debug.Log($"Sent to Python: {message}");
    }

    private async Task ReceiveMessagesAsync()
    {
       try
    {
        byte[] receivedData = new byte[1024];
        while (true)
        {
            int bytesReceived = await stream.ReadAsync(receivedData, 0, receivedData.Length);


            string receivedMessage = Encoding.UTF8.GetString(receivedData, 0, bytesReceived);

            OnSocketMessageReceive?.Invoke(this, new OnSocketMessageReceiveArg { message = receivedMessage });
        }
    }
    catch (Exception e)
    {
        Debug.LogError($"Socket error: {e}");
    }
       
    }
}
