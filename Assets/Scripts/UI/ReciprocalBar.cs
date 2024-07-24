using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReciprocalBar : MonoBehaviour
{
    Image img;
    public float AliveTime = 30;
    void Start()
    {
        img = GetComponent<Image>();
        img.fillAmount = 1;
        img.DOFillAmount(0, AliveTime).SetEase(Ease.Linear);
    }
}
