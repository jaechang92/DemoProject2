using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;
    public int taskID;
    [SerializeField]
    private List<GameObject> taskUIList;
    Dictionary<int, Task> taskList;
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
        taskCount += manager.gameRuleData.commonTask;
        taskCount += manager.gameRuleData.complexTask;
        taskCount += manager.gameRuleData.simpleTask;
        for (int i = 0; i < taskCount; i++)
        {
            int randomIdx = Random.Range(0, taskUIList.Count);
            if (!taskIndexs.Contains(randomIdx))
            {
                taskIndexs.Add(randomIdx);
            }
            else
            {
                i--;
            }
        }

        taskList = new Dictionary<int, Task>();
        
    }

    void GenerateData()
    {
        taskList.Add(0, new Task("ElectricalConnectMission", new int[] { 0 }));
    }

    public int GetTaskIndex(int id)
    {
        return taskID;
    }

}
