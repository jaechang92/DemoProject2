using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VibrationGageUI : MonoBehaviour
{
    public Image calibratorGauges;
    
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
    private float amountCenterValue;
    public float AmountCenterValue 
    { 
        get
        {
            return amountCenterValue;
        }
        set
        {
            amountCenterValue = value;
        }
    }
    [SerializeField]
    private float gageRange;
    public float GageRange 
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
    //[SerializeField]
    //private Vector2 gageRange;
    //public Vector2 GageRange 
    //{
    //    get
    //    {
    //        return gageRange;
    //    }
    //    set
    //    {
    //        gageRange = value;
    //    }
    //}
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
            calibratorGauges.fillAmount = Random.Range(amountCenterValue - gageRange, amountCenterValue + gageRange);
            currentTime = 0;
        }
    }
}
