using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineAlignmentMission : ClearChecker
{
    [SerializeField]
    private RectTransform engineAlign_engine;
    [SerializeField]
    private RectTransform engineAlign_slider;
    [SerializeField]
    private float circleCenterRange;
    [SerializeField]
    private bool trace = false;
    [SerializeField]
    private Vector2 pointer;
    [SerializeField]
    private RectTransform limitHeight;
    [SerializeField]
    private RawImage line;
    [SerializeField]
    private Color idleColor;
    [SerializeField]
    private Color nearColor;
    void Start()
    {

        /// 차후에 삼각형의 외심을 구하는 공식을 만들어야한다.
        pointer = new Vector2(Screen.width + 100, Screen.height / 2);
        circleCenterRange = Screen.width / 2 - engineAlign_slider.GetComponentInChildren<RectTransform>().anchoredPosition.x + 100;
        line.color = idleColor;
        float startHeight = Random.Range(0, Screen.height);
        Debug.Log(startHeight);
        while (startHeight< (Screen.height / 2) +100 && startHeight > (Screen.height / 2) - 100)
        {
            startHeight = Random.Range(-limitHeight.anchoredPosition.y, limitHeight.anchoredPosition.y);
        }
        Debug.Log(startHeight);
        GetVector2XY(new Vector2(100, startHeight));
    }

    void Update()
    {
        if (isClear)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.right, 1f);
            if (hit.collider != null &&  hit.collider.gameObject == engineAlign_slider.gameObject)
            {
                trace = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            trace = false;
        }

        if (trace)
        {
            GetVector2XY(Input.mousePosition);
        }

        if (engineAlign_slider.eulerAngles.z <= 5 && engineAlign_slider.eulerAngles.z >= -5)
        {
            line.color = nearColor;
        }
        else
        {
            line.color = idleColor;
        }


        if (engineAlign_slider.eulerAngles.z <= 1 && engineAlign_slider.eulerAngles.z >= -1)
        {
            engineAlign_slider.eulerAngles = Vector3.zero;
            isClear = true;
            InGameUIManager.Instance.CloseTaskUI(gameObject, 1.0f);
        }
    }

    private void GetVector2XY(Vector2 inputValue)
    {
        Vector2 mPos = inputValue;
        Vector2 dir = mPos - pointer;
        Vector2 temp = pointer + dir.normalized * circleCenterRange;
        engineAlign_slider.transform.position = temp;
        Vector2 temp2;
        RectTransform rt = engineAlign_slider.GetComponent<RectTransform>();
        if (rt.anchoredPosition.y > limitHeight.anchoredPosition.y)
        {
            temp2 = limitHeight.anchoredPosition;
            rt.anchoredPosition = temp2;
        }

        if (rt.anchoredPosition.y < -limitHeight.anchoredPosition.y)
        {
            temp2 = limitHeight.anchoredPosition;
            temp2.y = -temp2.y;
            rt.anchoredPosition = temp2;
        }
        engineAlign_slider.transform.right = new Vector3(pointer.x - engineAlign_slider.transform.position.x, pointer.y - engineAlign_slider.transform.position.y);

        Vector3 engineAlignAngle = engineAlign_engine.eulerAngles;
        engineAlignAngle = engineAlign_slider.GetComponent<RectTransform>().eulerAngles;
        engineAlign_engine.eulerAngles = engineAlignAngle;
    }

    //private Vector2 GetVector2XY()
    //{
    //    float angle = Mathf.Asin(Input.mousePosition.y/circleCenterRange);
    //    float conversionX = circleCenterRange * Mathf.Cos(angle);

    //    return new Vector2(pointer.x - conversionX, Input.mousePosition.x);

    //}

    private float GetAngle(Vector2 start, Vector2 end)
    {
        Vector2 v2 = end - start;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }


    
    private Vector2 CalculationOfCentroid()
    {



        return Vector2.zero;
    }


}
