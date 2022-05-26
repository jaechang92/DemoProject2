using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskExclamationMark : MonoBehaviour
{
    [SerializeField]
    private GameObject exclamationMark;

    [SerializeField]
    public List<GameObject> deactivateObjectPool;
    [SerializeField]
    public List<GameObject> activeObjectPool;



    void Start()
    {
        
    }

    public GameObject PopObject()
    {
        if (deactivateObjectPool.Count == 0)
        {
            CreateObjectInPool();
        }
        GameObject obj = deactivateObjectPool[0];
        obj.SetActive(true);
        activeObjectPool.Add(obj);
        deactivateObjectPool.RemoveAt(0);
        return obj;
    }

    public void PushObject()
    {
        foreach (var item in activeObjectPool)
        {
            deactivateObjectPool.Add(item);
            item.SetActive(false);
        }
        activeObjectPool.Clear();
    }

    public void CreateObjectInPool()
    {
        var obj = Instantiate(exclamationMark, transform);
        deactivateObjectPool.Add(obj);
    }
    public void DestoryObjectInPool()
    {

    }
}
