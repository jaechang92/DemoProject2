using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalMission : MonoBehaviour
{
    public WireTaskControl wireTaskObject;

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
        wireTaskObject.gameObject.SetActive(true);
        wireTaskObject.Open();
    }
}
