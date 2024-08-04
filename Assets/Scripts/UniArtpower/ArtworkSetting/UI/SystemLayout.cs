using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HimeLib;
using System.Threading.Tasks;

[RequireComponent(typeof(CanvasGroup))]
public class SystemLayout : SingletonMono<SystemLayout>
{
    [Header("透明按鈕 (用於開啟選單)")] public Button BTN_Option_Open;
    [Header("關閉選單")] public Button BTN_Option_Close;
    [Header("選單內容放置容器")] public CanvasGroup ContentCanvas;
    [Header("需一起隱藏物件")] public List<GameObject> needHides;
    public bool isActive => ContentCanvas.blocksRaycasts;

    public float clickInTime = 5;

    [Header("Runtime")]
    [SerializeField] float resetTimeRemain = 5;
    [SerializeField] int clickIndex = 0;
    
    async void Start()
    {
        BTN_Option_Open.onClick.AddListener(delegate {
            clickIndex++;

            if(clickIndex > 5){
                ShowOption(true);
                clickIndex = 0;
            }
        });

        BTN_Option_Close.onClick.AddListener(delegate {
            ShowOption(false);
        });

        await Task.Delay(10000);

        if(this == null)
            return;

        ShowOption(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F8)){
            ShowOption(!isActive);
        }

        resetTimeRemain -= Time.deltaTime;
        if(resetTimeRemain < 0){
            resetTimeRemain = clickInTime;
            clickIndex = 0;
        }
    }

    void ShowOption(bool val){
        ContentCanvas.blocksRaycasts = val;
        ContentCanvas.alpha = val ? 1 : 0;
        foreach (var item in needHides)
        {
            item.SetActive(val);
        }
    }
}
