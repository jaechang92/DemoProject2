using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinUI : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed;
    
    private Transform tr;
    private void Start()
    {
        tr = GetComponent<Transform>();
    }
    private void Update()
    {
        tr.Rotate(Vector3.forward, Time.deltaTime * spinSpeed);
    }

}
