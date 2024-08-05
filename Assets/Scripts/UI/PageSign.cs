using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;


public class PageSign : PageBase
{
    public Button BTN_Sumbit;
    public RectTransform Draw_Leaf;
    public CanvasGroup Draw_LeafAlpha;
    public PaintLight paintLight;
    public PainterMemory painterMemory;

    public VideoPlayer paintVideo;
    public CanvasGroup paintTip;

    [Header("飛葉動畫")]
    public Vector2 FlyPosition;
    public float FlySecond;
    public Ease easeType;

    public bool isContinueMode;

    public float dealyToTitle = 30;
    [SerializeField] float remainTime = 30;
    void Start()
    {
        OnPageShow += async () => {
            paintLight.DefaultUserSettingLight();
            painterMemory.Clear();
            Draw_Leaf.anchoredPosition = Vector2.zero;
            Draw_LeafAlpha.alpha = 1;
            paintTip.alpha = 1;
            paintVideo.Play();
            await Task.Delay(100);
            paintLight.canDrawing = true;
            BTN_Sumbit.interactable = true;
            remainTime = dealyToTitle;
        };
        OnPageHide += () => {
            paintLight.canDrawing = false;
        };

        BTN_Sumbit.onClick.AddListener(async () => {
            BTN_Sumbit.interactable = false;
            Draw_Leaf.DOAnchorPos(FlyPosition, FlySecond).SetEase(easeType);
            Draw_LeafAlpha.DOFade(0, FlySecond).SetEase(easeType);
            await Task.Delay(2000);
            
            StrokeReader.instance.CreateJSON();
            ESNetwork.instance.SendFinSign_OSC();

            if(!isContinueMode){
                LJMPageManager.instance.GotoPage(2);
            } else {
                paintLight.DefaultUserSettingLight();
                painterMemory.Clear();

                await Task.Delay(100);
                Draw_Leaf.anchoredPosition = Vector2.zero;
                Draw_LeafAlpha.alpha = 1;
                BTN_Sumbit.interactable = true;
            }
        });

        paintLight.OnDrawStart += x => {
            paintTip.alpha = 0;
            paintVideo.Stop();
        };

        paintLight.OnDrawDrag += x => {
            remainTime = dealyToTitle;
        };

        StartCoroutine(LoopCheck());
    }

    IEnumerator LoopCheck(){
        WaitForSeconds wait = new WaitForSeconds(1);
        while (true)
        {
            if(remainTime > 0 && IsVisible()){
                remainTime = remainTime - 1;
                if(remainTime <=0){
                    LJMPageManager.instance.GotoPage(0);
                    ESNetwork.instance.SendHome_OSC();
                }
            }
            yield return wait;
        }
    }
}
