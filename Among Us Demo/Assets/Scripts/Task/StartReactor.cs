using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartReactor : ClearChecker
{
    [SerializeField]
    private Color colorCode;
    [SerializeField]
    private Color colorCodeRed;
    [SerializeField]
    private List<Image> leftLights;
    [SerializeField]
    private List<Image> rightLights;
    [SerializeField]
    private List<GameObject> screenButtonImage;
    [SerializeField]
    private List<GameObject> ssbuttonList;

    [SerializeField]
    private int missionIdx = 1;

    [SerializeField]
    private List<int> missionIdxList;

    [SerializeField]
    private int checkIdx = 0;

    [SerializeField]
    private float reTryTime;
    [SerializeField]
    private float currentTime = 0;
    private bool isClickDelay = false;
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            int randomIdx = Random.Range(0, 9);
            if (!missionIdxList.Contains(randomIdx))
            {
                missionIdxList.Add(randomIdx);
            }
            else
            {
                i--;
            }
        }

        for (int i = 0; i < screenButtonImage.Count; i++)
        {
            screenButtonImage[i].SetActive(false);
        }


    }

    private void OnEnable()
    {
        StartCoroutine(Co_StartReactor());

    }

    void Update()
    {
        if (!isClickDelay)
        {
            currentTime += Time.deltaTime;
            if (reTryTime < currentTime)
            {
                StartCoroutine(Co_StartReactor());
                currentTime = 0;
            }
        }
    }


    IEnumerator Co_StartReactor()
    {
        isClickDelay = true;
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < rightLights.Count; i++)
        {
            rightLights[i].color = Color.white;
        }

        leftLights[missionIdx - 1].color = colorCode;

        for (int i = 0; i < missionIdx; i++)
        {
            screenButtonImage[missionIdxList[i]].SetActive(true);
            yield return new WaitForSeconds(.5f);
            screenButtonImage[missionIdxList[i]].SetActive(false);
            yield return new WaitForSeconds(.1f);
        }
        isClickDelay = false;
    }

    public void ClickButton(int idx)
    {
        currentTime = 0;
        if (missionIdxList[checkIdx] == idx)
        {
            Debug.Log("next");
            rightLights[checkIdx].color = colorCode;
            checkIdx++;
            if (missionIdx == checkIdx)
            {
                checkIdx = 0;
                missionIdx++;
                if (missionIdx <= missionIdxList.Count)
                {
                    
                    StartCoroutine(Co_StartReactor());
                }
                else
                {
                    isClear = true;
                    InGameUIManager.Instance.CloseTaskUI(gameObject, 1.0f);
                }
                
            }
        }
        else
        {
            // ??????????????? ??????
            rightLights[checkIdx].color = colorCodeRed;
            StartCoroutine(Co_StartReactor());
        }
    }

}
