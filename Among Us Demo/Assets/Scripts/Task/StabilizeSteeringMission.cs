using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StabilizeSteeringMission : MonoBehaviour
{
    [SerializeField]
    private float gizmoRadius;
    [SerializeField]
    private float clearRadius;
    [SerializeField]
    private Transform nav_stabilize_target;
    [SerializeField]
    private bool isClear = false;
    private bool trace = false;

    private void Start()
    {
        while (Vector3.Distance(nav_stabilize_target.position, transform.position) <= clearRadius)
        {
            nav_stabilize_target.position = RandomSphereInPoint(200f);
        }
    }


    void Update()
    {
        if (isClear)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && Vector3.Distance(nav_stabilize_target.position, Input.mousePosition) <= gizmoRadius)
        {
            trace = true;
        }

        if (trace)
        {
            nav_stabilize_target.position = Input.mousePosition;
            if (Vector3.Distance(nav_stabilize_target.position, transform.position) <= clearRadius)
            {
                nav_stabilize_target.position = transform.position;
                trace = false;
                isClear = true;
                StartCoroutine(ColorChange());
                return;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            trace = false;
        }
    }

    IEnumerator ColorChange()
    {
        int count = 2;
        var changeImages = nav_stabilize_target.GetComponentsInChildren<Image>();
        while (count > 0)
        {
            count--;
            foreach (var item in changeImages)
            {
                item.color = Color.yellow;
            }
            yield return new WaitForSeconds(.125f);
            foreach (var item in changeImages)
            {
                item.color = Color.white;
            }
            yield return new WaitForSeconds(.125f);
        }
        foreach (var item in changeImages)
        {
            item.color = Color.green;
        }
        Invoke("CloseUI", 1.0f);
    }


    private void CloseUI()
    {
        gameObject.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(nav_stabilize_target.position, gizmoRadius);
        Gizmos.DrawWireSphere(transform.position, clearRadius);
    }

    private Vector3 RandomSphereInPoint(float radius)
    {
        Vector3 getPoint = Random.onUnitSphere;
        getPoint.z = 0.0f;
        float r = Random.Range(0.0f, radius);
        return (getPoint * r) + transform.position;
    }

}
