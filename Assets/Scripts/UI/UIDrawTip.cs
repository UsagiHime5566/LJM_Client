using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrawTip : MonoBehaviour, IPointerDownHandler
{
    public CanvasGroup canvasGroup;
    void Awake()
    {
        if(canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canvasGroup.alpha = 0;
        Debug.Log("1111");
    }

    public float alpha {
        set { canvasGroup.alpha = value; }
    }
}
