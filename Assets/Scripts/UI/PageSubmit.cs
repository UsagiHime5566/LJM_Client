using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PageSubmit : PageBase
{
    public float WaitTime;
    public Image IMG_Remain;

    [Header("閃爍文字")]
    public CanvasGroup IMG_ShingText;
    public float FadeTime = 3;


    [SerializeField]
    float remainSecond;

    void Start()
    {
        var tween = IMG_ShingText.DOFade(0, FadeTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InQuart);

        OnPageShow += () => {
            remainSecond = WaitTime;
            tween.Restart();
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

        //IMG_Remain.fillAmount = (remainSecond/WaitTime);
    }
}
