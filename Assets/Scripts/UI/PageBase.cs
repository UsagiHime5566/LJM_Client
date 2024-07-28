using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PageBase : MonoBehaviour
{
    CanvasGroup canvasGroup;
    RectTransform rectTransform;

    public Action OnPageShow;
    public Action OnPageHide;

    void Awake(){
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        canvasGroup.alpha = 0;
        rectTransform.anchoredPosition = Vector2.zero;

    }

    public void Show(){
        OnPageShow?.Invoke();
        canvasGroup.DOFade(1, 0.35f);
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide(){
        OnPageHide?.Invoke();
        canvasGroup.DOFade(0, 0.35f);
        canvasGroup.blocksRaycasts = false;
    }

    public bool IsVisible(){
        return canvasGroup.alpha == 1 && canvasGroup.blocksRaycasts;
    }
}
