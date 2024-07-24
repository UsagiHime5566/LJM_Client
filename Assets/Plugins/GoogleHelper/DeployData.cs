using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class DeployData : MonoBehaviour
{
    public string deployTicketURL = "https://script.google.com/macros/s/AKfycbxX7JfA21R1jw4DIXVNyQHAkz0epcaZN2Qa5IIgnmBtqhH15-KMCObUT9jOPDSqplQx/exec";
    public string para1 = "tid";
    public string para2 = "type";

    public async void DeployNewTicket(string tid, string t_type){
        string result = await DeployTicketWWWCall(tid, t_type);
        Debug.Log($"Web result: {result}");
    }

    async Task<string> DeployTicketWWWCall(string tid, string t_type)
    {
        if (string.IsNullOrEmpty(deployTicketURL) || string.IsNullOrEmpty(para1) || string.IsNullOrEmpty(para2))
            return null;

        string query = $"{deployTicketURL}?{para1}={tid}&{para2}={t_type}";

        UnityWebRequest www = CreateWWWcall(query);
        string result = string.Empty;
        if (www == null)
            Debug.LogError("Unable to call www");
        else
        {
            Debug.Log("www is runing ...");
            result = await WaitForWWW(www);
        }
        return await Task.FromResult<string>(result);
    }

    UnityWebRequest CreateWWWcall(string query)
    {
        UnityWebRequest www = UnityWebRequest.Get(query);

#if UNITY_2017_2_OR_NEWER
        www.SendWebRequest();
#else
        www.Send();
#endif
        return www;
    }

    async Task<string> WaitForWWW(UnityWebRequest www)
    {
        string result = string.Empty;

        while (true)
        {
            if (www == null)
                break;

            if (www.isDone) {
                string Error = www.error;

                if (string.IsNullOrEmpty(Error)) {
                    result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    bool isEmpty = string.IsNullOrEmpty(result) || result == "\"\"";

                    if (isEmpty){
                        Debug.LogError("No data in downloaded");
                        result = string.Empty;
                    }

                    Debug.Log("www finished.");
                }
                else {
                    Debug.Log("Unable to access www : " + Error);
                }

                break;
            }
            await Task.Yield();
        }
        return await Task.FromResult<string>(result);
    }
}
