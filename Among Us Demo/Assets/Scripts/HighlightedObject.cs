using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HighlightedObject : MonoBehaviour
{
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var character = collision.GetComponent<CharacterMover>();
        if (character != null && character.hasAuthority)
        {
            spriteRenderer.material.SetFloat("_Highlighted", 0);
        }
    }




}
