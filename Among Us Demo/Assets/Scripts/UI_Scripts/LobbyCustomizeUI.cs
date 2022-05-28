using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCustomizeUI : MonoBehaviour
{

    [SerializeField]
    private Sprite useButtonSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LobbyUIManager.Instance.SetUesButton(useButtonSprite, OnClickUseButton_InLobby);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LobbyUIManager.Instance.UnSetUesButton();
    }

    public void OnClickUseButton_InLobby()
    {
        LobbyUIManager.Instance.CustomizeUI.Open();
    }
}
