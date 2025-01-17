using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageView : PageBase
{
    public Button BTN_Back;
    public Button BTN_PageLeft;
    public Button BTN_PageRight;
    void Start()
    {
        BTN_Back.onClick.AddListener(() => {
            LJMPageManager.instance.GotoPage(0);
            ESNetwork.instance.SendHome_OSC();
        });

        BTN_PageLeft.onClick.AddListener(() => {
            ESNetwork.instance.SendPageLeft_OSC();
        });

        BTN_PageRight.onClick.AddListener(() => {
            ESNetwork.instance.SendPageRight_OSC();
        });

        OnPageShow += () => {
            ESNetwork.instance.SendHandShake();
        };
    }

    public void SetLeftArrow(int i){
        BTN_PageLeft.interactable = i == 1 ? true : false;
    }

    public void SetRightArrow(int i){
        BTN_PageRight.interactable = i == 1 ? true : false;
    }
}
