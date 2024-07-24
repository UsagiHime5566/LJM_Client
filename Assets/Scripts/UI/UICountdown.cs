using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class UICountdown : MonoBehaviour
{
    public VideoPlayer VP_Countdown;
    public Image IMG_Countdown;
    public CanvasGroup visible;

    public List<Sprite> countdownSprite;
    
    int countdown;



    public void StartCountdown(float val, System.Action callback){
        countdown = Mathf.Clamp(Mathf.FloorToInt(val), 0, 99);

        float newPlaybackSpeed = (float)VP_Countdown.length / val;
        VP_Countdown.playbackSpeed = newPlaybackSpeed;
        VP_Countdown.time = 0;
        VP_Countdown.Play();

        visible.DOComplete();
        visible.DOKill();
        visible.alpha = 1;

        StopCountdown();
        StartCoroutine(LoopCountdown(callback));
    }

    IEnumerator LoopCountdown(System.Action callback){
        IMG_Countdown.sprite = countdownSprite[Mathf.Clamp(countdown, 0, 99)];
        while(countdown > 0){
            yield return new WaitForSeconds(1);

            countdown--;
            IMG_Countdown.sprite = countdownSprite[Mathf.Clamp(countdown, 0, 99)];

            if(countdown == 10){
                visible.DOFade(0, 0.7f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            }
        }
        callback?.Invoke();
    }

    public void StopCountdown(){
        StopAllCoroutines();
    }
}
