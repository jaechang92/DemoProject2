using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerDistributorMission : MonoBehaviour
{
    [SerializeField]
    private List<Transform> calibratorSpins;
    [SerializeField]
    private List<Image> calibratorContactBaseLits;

    [SerializeField]
    private List<VibrationGageUI> calibratorGauges;
    [SerializeField]
    private List<Button> calibratorButtons;
    [SerializeField]
    private float rotSpeed;
    [SerializeField]
    private int nowIdx = 0;
    [SerializeField]
    private int correctRange;
    [SerializeField]
    private float vibrationTIme = 0.0625f;
    [SerializeField]
    private Vector4 minMaxPair;
    [SerializeField]
    private bool isClear = false;
    private void init()
    {
        //-140
        nowIdx = 0;
        for (int i = 0; i < calibratorSpins.Count; i++)
        {
            calibratorSpins[i].eulerAngles = new Vector3(0, 0, -140);
            calibratorGauges[i].VibrationTIme = vibrationTIme;
            calibratorGauges[i].GageRange = new Vector2(minMaxPair.x, minMaxPair.y);
            calibratorContactBaseLits[i].gameObject.SetActive(false);
        }

    }

    void Start()
    {
        init();
        for (int i = 0; i < calibratorButtons.Count; i++)
        {
            int temp = i;
            calibratorButtons[i].onClick.AddListener(() => ClickButton(temp));
        }
    }
    [SerializeField]
    private float currentTime;
    void Update()
    {
        if (nowIdx >= calibratorSpins.Count)
        {
            return;
        }
        if (nowIdx < calibratorSpins.Count)
        {
            calibratorSpins[nowIdx].Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
        }

        if (calibratorSpins[nowIdx].eulerAngles.z <= correctRange && calibratorSpins[nowIdx].eulerAngles.z >= -correctRange)
        {
            calibratorGauges[nowIdx].GageRange = new Vector2(minMaxPair.z, minMaxPair.w);
            calibratorContactBaseLits[nowIdx].gameObject.SetActive(true);
        }
        else
        {
            calibratorGauges[nowIdx].GageRange = new Vector2(minMaxPair.x, minMaxPair.y);
            calibratorContactBaseLits[nowIdx].gameObject.SetActive(false);
        }
    }

    private void ClickButton(int idx)
    {
        Debug.Log(idx);
        if (idx == nowIdx)
        {
            if (calibratorSpins[nowIdx].eulerAngles.z <= correctRange && calibratorSpins[nowIdx].eulerAngles.z >= -correctRange)
            {
                calibratorContactBaseLits[nowIdx].gameObject.SetActive(true);
                calibratorSpins[nowIdx].eulerAngles = Vector3.zero;
                calibratorGauges[nowIdx].GageRange = new Vector2(minMaxPair.z, minMaxPair.w);
                //StartCoroutine(VibrationGage(nowIdx));
                
                nowIdx++;
                if (nowIdx == calibratorGauges.Count)
                {
                    isClear = true;
                    Invoke("CloseUI", 1.0f);
                }
            }
            else
            {
                init();
            }
        }
    }


    private void CloseUI()
    {
        gameObject.SetActive(false);
    }


}
