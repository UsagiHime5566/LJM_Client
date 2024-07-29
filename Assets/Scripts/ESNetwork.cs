using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using OscJack;
using System.Text;

public class ESNetwork : HimeLib.SingletonMono<ESNetwork>
{
    public string receiverIPAddress; // 接收端的IP地址
    public int oscPort = 25566;
    public int tcpPort = 25544;

    public void SendStart_OSC(){
        // IP address, port number
        using (var client = new OscClient(receiverIPAddress, oscPort))
        {
            client.Send("/signing");
        }
    }

    public void SendFinSign_OSC(){
        // IP address, port number
        using (var client = new OscClient(receiverIPAddress, oscPort))
        {
            client.Send("/signed");
        }
    }

    public void SendAffiliated_OSC(){
        // IP address, port number
        using (var client = new OscClient(receiverIPAddress, oscPort))
        {
            client.Send("/Affiliated");
        }
    }

    public async void SendStrokeToDisplay(string data){
        byte[] bytesToSend = Encoding.UTF8.GetBytes(data);
        await SendFileAsync(bytesToSend);
    }
    
    public async void SendToWall(byte[] bytes)
    {
        await SendFileAsync(bytes);
    }

    async Task SendFileAsync(byte[] bytes)
    {
        try
        {
            // 建立TCP客戶端
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync(receiverIPAddress, tcpPort);

                // 獲取網絡流
                using (NetworkStream stream = client.GetStream())
                {
                    // 發送PNG文件的字節數組
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error sending file: {e.Message}");
        }
    }
}
