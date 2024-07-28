using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJMPageManager : HimeLib.SingletonMono<LJMPageManager>
{
    public List<PageBase> pages;

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
}
