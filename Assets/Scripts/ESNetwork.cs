using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using OscJack;


public class ESNetwork : HimeLib.SingletonMono<ESNetwork>
{
    public string receiverIPAddress; // 接收端的IP地址
    public List<int> ports = new List<int>(){25560, 25561}; // 通訊端口
    
    public async void SendToWall(byte[] bytes, int portIndex)
    {
        await SendFileAsync(bytes, portIndex);
    }

    async Task SendFileAsync(byte[] bytes, int portIndex)
    {
        try
        {
            // 建立TCP客戶端
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync(receiverIPAddress, ports[portIndex]);

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
