using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField]
    private Button MouseControlButton;
    [SerializeField]
    private Button KeyboardMouseControlButton;
    private Animator animator;
    
    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        switch (PlayerSettins.controlType)
        {
            case EControlType.Mouse:
                MouseControlButton.image.color = Color.green;
                KeyboardMouseControlButton.image.color = Color.white;
                break;
            case EControlType.KeyboardMouse:
                MouseControlButton.image.color = Color.white;
                KeyboardMouseControlButton.image.color = Color.green;
                break;
            default:
                break;
        }
    }

    public void SetControlMode(int controlType)
    {
        PlayerSettins.controlType = (EControlType)controlType;
        switch (PlayerSettins.controlType)
        {
            case EControlType.Mouse:
                MouseControlButton.image.color = Color.green;
                KeyboardMouseControlButton.image.color = Color.white;
                break;
            case EControlType.KeyboardMouse:
                MouseControlButton.image.color = Color.white;
                KeyboardMouseControlButton.image.color = Color.green;
                break;
            default:
                break;
        }
    }

    public virtual void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("Close");
    }
}
