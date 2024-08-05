using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LJMPageManager : HimeLib.SingletonMono<LJMPageManager>
{
    public List<PageBase> pages;
    public CanvasGroup SayOverlay;

    int currentPage = 0;
    void Start()
    {
        pages[0].Show();
        currentPage = 0;
    }

    public void GotoPage(int pageIndex){
        pages[currentPage].Hide();
        pages[pageIndex].Show();
        currentPage = pageIndex;
    }

    public void VisibleOverlaySay(bool val){
        if(val){
            SayOverlay.DOFade(1, 0.7f);
            SayOverlay.blocksRaycasts = true;
        } else {
            SayOverlay.DOFade(0, 0.7f);
            SayOverlay.blocksRaycasts = false;
        }
    }
}
