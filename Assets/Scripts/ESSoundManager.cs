using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ESSoundManager : HimeLib.SingletonMono<ESSoundManager>
{
    public AudioClip BGM_Title;
    public AudioClip BGM_Game;
    public AudioClip SFX_Play;
    public AudioClip SFX_Button;
    public AudioClip SFX_Submit;
    public AudioClip SFX_Draw;

    [Header("系統元件")]
    public AudioSource BGMPlayer;
    public AudioSource SFXPlayer;
    public AudioSource SFXPlayerDraw;

    [Header("系統參數")]
    public float bgmChangeDelay = 0.35f;
    public float bgmFadeTime = 0.25f;

    [Header("Runtime")]
    [SerializeField] bool isDrawing;

    public void PlayBGM(AudioClip clip){
        if(BGMPlayer.clip == clip){
            return;
        } else {
            Sequence seq = DOTween.Sequence();
            seq.Append(BGMPlayer.DOFade(0, bgmFadeTime))
                .AppendInterval(bgmChangeDelay)
                .AppendCallback(() => {
                    BGMPlayer.clip = clip;
                    BGMPlayer.Play();
                })
                .Append(BGMPlayer.DOFade(1, bgmFadeTime));
        }
    }

    public void SetBGMVolumn(float val){
        BGMPlayer.DOFade(val, bgmFadeTime);
    }

    public void PlayBGMTitle(){
        PlayBGM(BGM_Title);
    }

    public void PlayBGMGame(){
        PlayBGM(BGM_Game);
    }

    public void StopBGM(){
        BGMPlayer.DOFade(0, bgmFadeTime).OnComplete(() => {
            BGMPlayer.clip = null;
            BGMPlayer.Stop();
        });
    }

    public void PlayStartGame(){
        SFXPlayer.PlayOneShot(SFX_Play);
    }

    public void PlayButton(){
        SFXPlayer.PlayOneShot(SFX_Button);
    }

    public void PlaySubmit(){
        SFXPlayer.PlayOneShot(SFX_Submit);
    }

    public void PlayDraw(){
        if (!SFXPlayerDraw.isPlaying)
        {
            SFXPlayerDraw.Play();
            SFXPlayerDraw.loop = true;
        }
    }

    public void StopDraw(){
        SFXPlayerDraw.Stop();
    }
}
