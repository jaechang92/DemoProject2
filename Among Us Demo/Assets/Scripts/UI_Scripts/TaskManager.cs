using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public int taskID;

    Dictionary<int, Task> taskList;


    
    void Start()
    {
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
