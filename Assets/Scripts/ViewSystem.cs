using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewSystem : MonoBehaviour
{
    public InputField INP_ServerIP;

    // public InputField INP_IdleToPaint;
    // public InputField INP_IdleSendToTitle;
    // public InputField INP_IdleToAd;
    // public InputField INP_UrlAD;
    // public InputField INP_OutputDrawPath;
    // public InputField INP_TargetIP;
    // public Toggle TG_UseNetMode;

    void Start(){

        #if UNITY_STANDALONE_WIN
            Screen.SetResolution(960, 540, FullScreenMode.Windowed);
        #endif

        INP_ServerIP.onValueChanged.AddListener(x => {
            ESNetwork.instance.receiverIPAddress = x;
            SystemConfig.Instance.SaveData("ServerIP", x);
        });
        INP_ServerIP.text = SystemConfig.Instance.GetData<string>("ServerIP", "127.0.0.1");
    }

    // void Start()
    // {
    //     INP_IdleToPaint.onValueChanged.AddListener(x => {
    //         float.TryParse(x, out float f);
    //         ESGameManager.instance.IdleToPaint = f;
    //         SystemConfig.Instance.SaveData("IdleToPaint", f);
    //     });
    //     INP_IdleToPaint.text = SystemConfig.Instance.GetData<float>("IdleToPaint", 90).ToString();

    //     INP_IdleSendToTitle.onValueChanged.AddListener(x => {
    //         float.TryParse(x, out float f);
    //         ESGameManager.instance.IdleSendToTitle = f;
    //         SystemConfig.Instance.SaveData("IdleSendToTitle", f);
    //     });
    //     INP_IdleSendToTitle.text = SystemConfig.Instance.GetData<float>("IdleSendToTitle", 10).ToString();

    //     INP_IdleToAd.onValueChanged.AddListener(x => {
    //         float.TryParse(x, out float f);
    //         ESGameManager.instance.IdleToAd = f;
    //         SystemConfig.Instance.SaveData("IdleToAd", f);
    //     });
    //     INP_IdleToAd.text = SystemConfig.Instance.GetData<float>("IdleToAd", 180).ToString();

    //     INP_UrlAD.onValueChanged.AddListener(x => {
    //         ESAdManager.instance.adPaths[0] = x;
    //         SystemConfig.Instance.SaveData("UrlAD", x);
    //     });
    //     INP_UrlAD.text = SystemConfig.Instance.GetData<string>("UrlAD", "");

    //     INP_OutputDrawPath.onValueChanged.AddListener(x => {
    //         ESGameManager.instance.OutputDrawPath = x;
    //         SystemConfig.Instance.SaveData("OutputDrawPath", x);
    //     });
    //     INP_OutputDrawPath.text = SystemConfig.Instance.GetData<string>("OutputDrawPath", "");

    //     INP_TargetIP.onValueChanged.AddListener(x => {
    //         ESNetwork.instance.receiverIPAddress = x;
    //         SystemConfig.Instance.SaveData("TargetIP", x);
    //     });
    //     INP_TargetIP.text = SystemConfig.Instance.GetData<string>("TargetIP", "127.0.0.1");

    //     TG_UseNetMode.onValueChanged.AddListener(x => {
    //         ESGameManager.instance.UseNetMode = x;
    //         SystemConfig.Instance.SaveData("UseNetMode", x);
    //     });
    //     TG_UseNetMode.isOn = SystemConfig.Instance.GetData<bool>("UseNetMode", false);
    // }
}
