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

    private AmongUsRoomManager manager;
    private void Start()
    {
        manager = NetworkManager.singleton as AmongUsRoomManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InGameUIManager.Instance.SetUesButton(SetVentUseButton);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InGameUIManager.Instance.UnSetUesButton();
    }

    public void SetVentUseButton()
    {
        VentSystem.Instance.SetVent((int)ventCategory, gameObject);
    }

}
