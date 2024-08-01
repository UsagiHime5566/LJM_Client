using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class UIShingSelf : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float FadeTime = 1.5f;
    public Ease easeType = Ease.InQuart;

    void Awake(){
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        var tween = canvasGroup.DOFade(0, FadeTime).SetLoops(-1, LoopType.Yoyo).SetEase(easeType);
    }
}
