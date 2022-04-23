using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VibrationGageUI : MonoBehaviour
{
    private Image calibratorGauges;
    
    [SerializeField]
    private float vibrationTIme;
    public float VibrationTIme 
    {
        set
        {
            vibrationTIme = value;
        }
    }
    [SerializeField]
    private Vector2 gageRange;
    public Vector2 GageRange 
    {
        get
        {
            return gageRange;
        }
        set
        {
            gageRange = value;
        }
    }
    void Start()
    {
        calibratorGauges = GetComponent<Image>();
    }

    [SerializeField]
    private float currentTime;
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > vibrationTIme)
        {
            calibratorGauges.fillAmount = Random.Range(gageRange.x, gageRange.y);
            currentTime = 0;
        }
    }
}
