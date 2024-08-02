using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageView : PageBase
{
    public Button BTN_Back;
    void Start()
    {
        BTN_Back.onClick.AddListener(() => {
            LJMPageManager.instance.GotoPage(0);
            ESNetwork.instance.SendHome_OSC();
        });
    }
}
