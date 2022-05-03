using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskExclamationMark : MonoBehaviour
{
    

    [SerializeField]
    private GameObject exclamationMark;

    [SerializeField]
    private List<GameObject> objectPool;



    void Start()
    {
        for (int i = 0; i < TaskManager.instance.taskCount; i++)
        {
            var obj = Instantiate(exclamationMark);
            objectPool.Add(obj);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
