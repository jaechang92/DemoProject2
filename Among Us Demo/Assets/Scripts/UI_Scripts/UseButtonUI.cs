using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UseButtonUI : MonoBehaviour
{
    [SerializeField]
    public Button useButton;

    bool canUse = false;


    public void SetUesButton(UnityAction action)
    {
        //useButton.image.sprite = sprite;
        useButton.onClick.AddListener(action);
        useButton.interactable = true;
    }


    public void OnClickUseButton()
    {
        // 사용 행동
    }

}
