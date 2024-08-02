using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISoundButton : MonoBehaviour, IPointerClickHandler
{
    public Button BTN_Raw;
    public int BTNType = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        // if(BTN_Raw == null){
        //     ESSoundManager.instance.PlayButton();
        // }
    }

    void Awake(){
        BTN_Raw = GetComponent<Button>();
    }

    void Start()
    {

        BTN_Raw?.onClick.AddListener(() => {
            if(BTNType == 0)
                ESSoundManager.instance.PlayStartGame();
            if(BTNType == 1)
                ESSoundManager.instance.PlayButton();
            if(BTNType == 2)
                ESSoundManager.instance.PlaySubmit();
        });
    }
}
