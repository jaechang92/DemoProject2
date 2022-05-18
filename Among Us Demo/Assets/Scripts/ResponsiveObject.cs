using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveObject : TaskData
{
    // 해당 객체에 할당하고 각각 퀘스트와 연결

    [SerializeField]
    private GameObject activeObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<CharacterMover>();
        if (character != null && character.hasAuthority)
        {
            InGameUIManager.Instance.SetUesButton(null, OnClickUse);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var character = collision.GetComponent<CharacterMover>();
        if (character != null && character.hasAuthority)
        {
            InGameUIManager.Instance.UnSetUesButton();
        }
    }

    public void OnClickUse()
    {
        InGameUIManager.Instance.TaskUI.SetActive(true);
        TaskManager.instance.TaskObject = gameObject;
        activeObject.SetActive(true);
        var childObject = activeObject.GetComponentsInChildren<Transform>();
        foreach (var child in childObject)
        {
            if (child.gameObject.activeSelf == false)
            {
                child.gameObject.SetActive(true);
            }
        }
        AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = false;
    }
}
