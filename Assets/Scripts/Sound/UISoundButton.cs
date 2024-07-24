using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISoundButton : MonoBehaviour, IPointerClickHandler
{
    public Button BTN_Raw;
    //public Toggle BTN_Toggle;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(BTN_Raw == null){
            ESSoundManager.instance.PlayButton();
        }
    }

    void Awake(){
        BTN_Raw = GetComponent<Button>();
        //BTN_Toggle = GetComponent<Toggle>();
    }

    void Start()
    {

        BTN_Raw?.onClick.AddListener(() => {
            ESSoundManager.instance.PlayButton();
        });

        // BTN_Toggle?.onValueChanged.AddListener(x => {
        //     ESSoundManager.instance.PlayButton();
        // });
    }
}
