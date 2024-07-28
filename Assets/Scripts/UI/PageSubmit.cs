using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSubmit : PageBase
{
    public float WaitTime;
    public Image IMG_Remain;

    [SerializeField]
    float remainSecond;

    void Start()
    {
        OnPageShow += () => {
            remainSecond = WaitTime;
        };
    }

    void LeavePage(){
        LJMPageManager.instance.GotoPage(0);
    }

    void Update()
    {
        if(!IsVisible()) return;

        if(remainSecond > 0){
            remainSecond -= Time.deltaTime;
        } else {
            LeavePage();
        }
        IMG_Remain.fillAmount = (remainSecond/WaitTime);
    }
}
