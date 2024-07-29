using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Compression;
using System.Text;

public class StrokeReader : HimeLib.SingletonMono<StrokeReader>
{
    public PainterMemory painterMemory;

    [EasyButtons.Button]
    void ReplayStoke(){
        string readCompressedJson = ReadFromFile("strokeData.txt");
        string decompressedJson = DecompressString(readCompressedJson);

        painterMemory.paintData = JsonUtility.FromJson<PaintData>(decompressedJson);
        painterMemory.Replay();
    }

    public void CreateJSON(){
        string jsonString = JsonUtility.ToJson(painterMemory.paintData);

        string compressedJson = CompressString(jsonString);

        // 將JSON字符串儲存到本地的txt文件上
        SaveToFile(compressedJson, "strokeData.txt");

        ESNetwork.instance.SendStrokeToDisplay(compressedJson);
    }

    void SaveToFile(string dataString, string fileName)
    {
        string path = Path.Combine(Application.dataPath, fileName);
        File.WriteAllText(path, dataString);
        Debug.Log("File saved to: " + path);
    }

    string ReadFromFile(string fileName)
    {
        string path = Path.Combine(Application.dataPath, fileName);
        return File.ReadAllText(path);
    }

    string CompressString(string str)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gzipStream.Write(bytes, 0, bytes.Length);
            }
            return System.Convert.ToBase64String(memoryStream.ToArray());
        }
    }

    string DecompressString(string compressedStr)
    {
        byte[] bytes = System.Convert.FromBase64String(compressedStr);
        using (MemoryStream memoryStream = new MemoryStream(bytes))
        {
            using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
            {
                using (MemoryStream decompressedStream = new MemoryStream())
                {
                    gzipStream.CopyTo(decompressedStream);
                    return Encoding.UTF8.GetString(decompressedStream.ToArray());
                }
            }
        }
    }
}
