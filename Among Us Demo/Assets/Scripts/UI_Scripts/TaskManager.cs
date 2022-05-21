using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class TaskManager : NetworkBehaviour
{
    public static TaskManager instance;
    public int taskID;
    [SerializeField]
    public List<ResponsiveObject> responsiveObject;
    [SerializeField]
    private List<ClearChecker> taskUIList;
    [SerializeField]
    public List<int> taskIndexs;
    public Text tasksTextUI;

    [SyncVar]
    public int clearCount;
    public int taskCount;

    [HideInInspector]
    public GameObject TaskObject;

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
        UpdateTaskText();
    }

    public void TaskClear(GameObject obj)
    {
        var TaskObjectColliders = TaskObject.GetComponentsInChildren<Collider2D>();
        foreach (var collider in TaskObjectColliders)
        {
            collider.enabled = false;
        }

        for (int i = 0; i < taskIndexs.Count; i++)
        {
            if (responsiveObject[taskIndexs[i]].gameObject == obj)
            {
                taskIndexs.Remove(i);
                break;
            }
        }
        UpdateTaskText();
    }

    public string taskText = null;
    private void UpdateTaskText()
    {
        taskText = null;
        for (int i = 0; i < taskIndexs.Count; i++)
        {
            if (taskUIList[taskIndexs[i]].isClear == true)
            {
                taskText += "<b><color=green>" + responsiveObject[taskIndexs[i]].taskName + "</color></b>";
            }
            else
            {
                taskText += "<b><color=white>" + responsiveObject[taskIndexs[i]].taskName + "</color></b>";
            }
            if (i != taskIndexs.Count -1)
            {
                taskText += "\n";
            }
            
        }
        tasksTextUI.text = taskText;
    }



}
