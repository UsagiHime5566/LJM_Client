using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PageSign : PageBase
{
    public Button BTN_Sumbit;
    public RectTransform Draw_Leaf;
    public CanvasGroup Draw_LeafAlpha;
    public PaintLight paintLight;
    public PainterMemory painterMemory;
    public Vector2 FlyPosition;
    public float FlySecond;
    public Ease easeType;
    void Start()
    {
        OnPageShow += async () => {
            paintLight.DefaultUserSettingLight();
            painterMemory.Clear();
            Draw_Leaf.anchoredPosition = Vector2.zero;
            Draw_LeafAlpha.alpha = 1;
            await Task.Delay(100);
            paintLight.canDrawing = true;
        };
        OnPageHide += () => {
            paintLight.canDrawing = false;
        };

        BTN_Sumbit.onClick.AddListener(async () => {
            Draw_Leaf.DOAnchorPos(FlyPosition, FlySecond).SetEase(easeType);
            Draw_LeafAlpha.DOFade(0, FlySecond).SetEase(easeType);
            await Task.Delay(2000);
            LJMPageManager.instance.GotoPage(2);
            StrokeReader.instance.CreateJSON();
            ESNetwork.instance.SendFinSign_OSC();
        });
    }
}
