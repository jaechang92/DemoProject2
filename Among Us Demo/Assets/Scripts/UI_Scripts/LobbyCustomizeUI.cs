using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCustomizeUI : MonoBehaviour
{

    [SerializeField]
    private Sprite useButtonSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<CharacterMover>();
        if (character != null && character.hasAuthority)
        {
            LobbyUIManager.Instance.SetUesButton(useButtonSprite, OnClickUseButton_InLobby);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var character = collision.GetComponent<CharacterMover>();
        if (character != null && character.hasAuthority)
        {
            LobbyUIManager.Instance.UnSetUesButton();
        }
    }

    public void OnClickUseButton_InLobby()
    {
        LobbyUIManager.Instance.CustomizeUI.Open();
    }
}
