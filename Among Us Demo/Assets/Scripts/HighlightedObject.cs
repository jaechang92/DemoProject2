using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HighlightedObject : MonoBehaviour
{
    [SerializeField]
    private Sprite useButtonSprite;
    private SpriteRenderer spriteRenderer;


    private AmongUsRoomManager manager;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        var inst = Instantiate(spriteRenderer.material);
        spriteRenderer.material = inst;
        manager = NetworkManager.singleton as AmongUsRoomManager;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<CharacterMover>();
        if (character != null && character.hasAuthority)
        {
            spriteRenderer.material.SetFloat("_Highlighted", 1);
            if (manager.nowScene == manager.RoomScene)
            {
                LobbyUIManager.Instance.SetUesButton(useButtonSprite, OnClickUseButton_InLobby);
            }
            if (manager.nowScene == manager.GameplayScene)
            {
                InGameUIManager.Instance.SetUesButton(useButtonSprite);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var character = collision.GetComponent<CharacterMover>();
        if (character != null && character.hasAuthority)
        {
            spriteRenderer.material.SetFloat("_Highlighted", 0);
            if (manager.nowScene == manager.RoomScene)
            {
                LobbyUIManager.Instance.UnSetUesButton();
            }
        }
    }

    public void OnClickUseButton_InLobby()
    {
        LobbyUIManager.Instance.CustomizeUI.Open();
    }


}
