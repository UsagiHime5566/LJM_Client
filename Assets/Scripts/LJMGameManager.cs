using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LJMGameManager : HimeLib.SingletonMono<LJMGameManager>
{
    public string OutputDrawPath;

    [SerializeField]
    string lastFileName;
    void Start()
    {
        OutputDrawPath = Application.dataPath;
        Application.targetFrameRate = 60;
    }

    public void SaveAndSend(RenderTexture RenderTextureRef){
        if(string.IsNullOrEmpty(OutputDrawPath)) return;
        FolderDetect(OutputDrawPath);

        byte[] bytes = SavePng(RenderTextureRef, OutputDrawPath, true);

        RenderTextureRef.Release();
        Object.Destroy(RenderTextureRef);
    }

    void FolderDetect(string path){
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        catch (IOException ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public byte[] SavePng(RenderTexture RenderTextureRef, string path, bool saveDisk)
    {
        Texture2D tex = new Texture2D(RenderTextureRef.width, RenderTextureRef.height, TextureFormat.RGBA32, false);
        RenderTexture.active = RenderTextureRef;
        tex.ReadPixels(new Rect(0, 0, RenderTextureRef.width, RenderTextureRef.height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Object.Destroy(tex);

        RenderTexture.active = null;

        //Write to a file in the project folder
        //string path = Application.dataPath + "/../SavedPaint.png";
        string fileName = System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        string fullPath = Path.Combine(path, fileName);

        if(saveDisk){
            File.WriteAllBytes(fullPath, bytes);
            Debug.Log(bytes.Length/1024  + "Kb was saved as: " + fullPath);
        }

        lastFileName = fileName;

        return bytes;
    }
}
