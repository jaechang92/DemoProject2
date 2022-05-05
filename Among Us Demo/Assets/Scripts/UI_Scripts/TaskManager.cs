using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;
    public int taskID;
    [SerializeField]
    private List<ResponsiveObject> responsiveObject;
    [SerializeField]
    private List<GameObject> taskUIList;
    [SerializeField]
    private List<int> taskIndexs;
    public int taskCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
    void Start()
    {
        var manager = NetworkManager.singleton as AmongUsRoomManager;
        if (manager != null)
        {
            taskCount += manager.gameRuleData.commonTask;
            taskCount += manager.gameRuleData.complexTask;
            taskCount += manager.gameRuleData.simpleTask;
        }
        else
        {
            taskCount = 3;
        }
        for (int i = 0; i < taskCount; i++)
        {
            int randomIdx = Random.Range(0, responsiveObject.Count);
            if (!taskIndexs.Contains(randomIdx))
            {
                taskIndexs.Add(randomIdx);
            }
            else
            {
                i--;
            }
        }

        for (int i = 0; i < responsiveObject.Count; i++)
        {
            if (!taskIndexs.Contains(i))
            {
                var colliders = responsiveObject[i].GetComponentsInChildren<BoxCollider2D>();
                foreach (var item in colliders)
                {
                    item.enabled = false;
                }
                responsiveObject[i].enabled = false;
            }
        }


    }

}
