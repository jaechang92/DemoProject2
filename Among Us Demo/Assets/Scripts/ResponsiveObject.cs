using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveObject : MonoBehaviour
{
    // 해당 객체에 할당하고 각각 퀘스트와 연결

    [SerializeField]
    private GameObject activeObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<CharacterMover>();
        if (character != null && character.hasAuthority)
        {
            InGameUIManager.Instance.SetUesButton(OnClickUse);
            InGameUIManager.Instance.nowTask(GetComponent<TaskData>().taskId);
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
        activeObject.SetActive(true);
        var childObject = activeObject.GetComponentsInChildren<GameObject>();
        foreach (var child in childObject)
        {
            if (child.activeSelf == false)
            {
                child.SetActive(true);
            }
        }
        AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = false;
    }
}
