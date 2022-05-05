using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> taskList;

    private void Start()
    {
        for (int i = 0; i < taskList.Count; i++)
        {
            taskList[i].SetActive(false);
        }
    }

    public void Close()
    {
        for (int i = 0; i < taskList.Count; i++)
        {
            taskList[i].SetActive(false);
        }
        gameObject.SetActive(false);
        AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = true;
    }


}
