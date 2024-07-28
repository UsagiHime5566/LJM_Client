using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PageSign : PageBase
{
    public Button BTN_Sumbit;
    public PaintLight paintLight;
    public PainterMemory painterMemory;
    void Start()
    {
        OnPageShow += async () => {
            paintLight.DefaultUserSettingLight();
            painterMemory.Clear();
            await Task.Delay(100);
            paintLight.canDrawing = true;
        };
        OnPageHide += () => {
            paintLight.canDrawing = false;
        };

        BTN_Sumbit.onClick.AddListener(() => {
            LJMPageManager.instance.GotoPage(2);
        });
    }


}
