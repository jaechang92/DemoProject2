using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class FuelMission : ClearChecker, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Image fuelGage;

    [SerializeField]
    private float fuelGageTimer;
    [SerializeField]
    private bool isPress = false;

    void Update()
    {
        if (!isClear)
        {
            GetFuel();
        }

    }

    public void GetFuel()
    {
        if (isPress)
        {
            fuelGage.fillAmount +=  Time.deltaTime / fuelGageTimer;
        }
        else
        {
            fuelGage.fillAmount -= Time.deltaTime / fuelGageTimer;
        }

        if (fuelGage.fillAmount >= 1)
        {
            isClear = true;
            InGameUIManager.Instance.CloseTaskUI(gameObject, 1.0f);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("UIButton"))
        {
            isPress = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
    }

}
