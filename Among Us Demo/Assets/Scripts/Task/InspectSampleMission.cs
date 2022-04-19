using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InspectSampleMission : MonoBehaviour
{
    [SerializeField]
    private Image[] sampleButtons;
    [SerializeField]
    private Image[] medBay_liquids;
    [SerializeField]
    private Transform medBay_dispenser;
    [SerializeField]
    private float moveTime;
    [SerializeField]
    private Color liquidColor;
    [SerializeField]
    private Color liquidErrorColor;
    [SerializeField]
    private float missionWaitTimeForMinutes;
    [SerializeField]
    private bool isWaiting;
    
    
    [SerializeField]
    private bool isClear;
    [SerializeField]
    private DateTime checkTimeOrigin;
    [SerializeField]
    public TimeSpan interval;
    [SerializeField]
    private int missionObjectIdx;

    [SerializeField]
    private bool testTime;

    [SerializeField]
    private Animator ani;
    [SerializeField]
    private IEnumerator dispenser_Coroutine;

    private void OnEnable()
    {
        
        missionObjectIdx = sampleButtons.Length;
        if (!isWaiting)
        {
            ani.Play("Medbox");
        }
    }

    private void Update()
    {
        if (isWaiting)
        {
            interval = DateTime.Now - checkTimeOrigin;
            // missionWaitTimeForMinutes 보다 크다면 미션 waiting 종료
            if (!testTime && interval.Minutes >= missionWaitTimeForMinutes)
            {
                isWaiting = false;
                missionObjectIdx = UnityEngine.Random.Range(0, medBay_liquids.Length);
                medBay_liquids[missionObjectIdx].color = liquidErrorColor;
                foreach (var item in sampleButtons)
                {
                    item.color = Color.green;
                }
            }
            else if(interval.Seconds >= missionWaitTimeForMinutes)
            {
                isWaiting = false;
                missionObjectIdx = UnityEngine.Random.Range(0, medBay_liquids.Length);
                medBay_liquids[missionObjectIdx].color = liquidErrorColor;
                foreach (var item in sampleButtons)
                {
                    item.color = Color.green;
                }
            }
        }
    }

    IEnumerator medBay_dispenser_Coroutine()
    {
        int idx = 0;
        WaitForSeconds waitTime = new WaitForSeconds(moveTime);
        while (idx < medBay_liquids.Length)
        {
            medBay_dispenser.position = new Vector3(medBay_liquids[idx].gameObject.transform.position.x, medBay_dispenser.position.y, medBay_dispenser.position.z);
            medBay_liquids[idx].color = liquidColor;
            idx++;
            yield return waitTime;
        }

        if (!isWaiting)
        {
            isWaiting = true;
            checkTimeOrigin = DateTime.Now;
        }
    }

    public void MedBay_SampleButton()
    {
        int idx = 0;
        foreach (var item in sampleButtons)
        {
            if (item.gameObject == EventSystem.current.currentSelectedGameObject)
            {
                if (missionObjectIdx == idx)
                {
                    medBay_liquids[idx].color = liquidColor;
                    isClear = true;
                    Invoke("CloseUI", 1.0f);
                }
                break;
            }
            idx++;
        }
    }

    public void MedBay_Button()
    {
        if (dispenser_Coroutine == null)
        {
            dispenser_Coroutine = medBay_dispenser_Coroutine();
            StartCoroutine(dispenser_Coroutine);
        }
        //if (!isWaiting)
        //{
        //    isWaiting = true;
        //    checkTimeOrigin = DateTime.Now;
        //}
    }

    private void CloseUI()
    {
        gameObject.SetActive(false);
    }



}
