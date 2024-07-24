using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GooglePostForm
{
    // url 複製自google form ex: https://docs.google.com/forms/u/1/d/e/1FAIpQLScPuQtc6GcNPJxnt9xDKhwg_jUPxyaNwjA_XsGerzRdYTrSqg/formResponse
    // 以 formResponse 結尾
    // entry 用Chrome F12打開原始碼，尋找entry來複製

    public static async void PostTool(PostContent postContent){
        WWWForm form = new WWWForm();
        foreach (var entry in postContent.entries)
        {
            form.AddField("entry." + entry.id, entry.content);
        }

        using (UnityWebRequest www = UnityWebRequest.Post(postContent.url, form))
        {
            var asyncOperation = www.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                await System.Threading.Tasks.Task.Delay(100);
            }

            //if (www.isNetworkError || www.isHttpError)
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }
}

[System.Serializable]
public class PostContent
{
    public string url;
    public List<PostEntry> entries;
}

[System.Serializable]
public class PostEntry
{
    public string id;
    public string content;
}