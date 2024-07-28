using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageTitle : PageBase
{
    public Button BTN_Start;
    public Button BTN_ViewStatus;
    void Start()
    {
        BTN_Start.onClick.AddListener(() => {
            LJMPageManager.instance.GotoPage(1);
        });

        BTN_ViewStatus.onClick.AddListener(ShowStatus);
    }

    void ShowStatus(){
        Debug.Log("顯示聯屬");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
