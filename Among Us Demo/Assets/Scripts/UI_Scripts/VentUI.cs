using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public enum VentCategory
{
    ventCafeteriaAdminHallway,
    ventMedbayElectricalSecurity,
    ventReactorUpperEngine,
    ventReactorLowerEngine,
    ventNavigationWeapons,
    ventNavigationShields,
};

public class VentUI : MonoBehaviour
{
    public VentCategory ventCategory;
    [SerializeField]
    private Sprite useButtonSprite;
    private AmongUsRoomManager manager;
    private void Start()
    {
        manager = NetworkManager.singleton as AmongUsRoomManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InGameUIManager.Instance.SetUesButton(useButtonSprite, SetVentUseButton);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (VentSystem.Instance.used == false )
        {
            InGameUIManager.Instance.UnSetUesButton();
        }
    }

    public void SetVentUseButton()
    {
        InGameUIManager.Instance.debugText.text += "VentActive";
        VentSystem.Instance.SetVent((int)ventCategory, gameObject);
    }

}
