using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChartCourseMission : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> checkPoints;

    [SerializeField]
    private Transform nav_chartCourse_ship;
    [SerializeField]
    private int nowIdx;
    [SerializeField]
    private float range;
    [SerializeField]
    private bool isClear = false;
    [SerializeField]
    private List<List<Transform>> llt = new List<List<Transform>>();
    [SerializeField]
    private Transform LineParent;
    [SerializeField]
    private GameObject LineObject;
    [SerializeField]
    private List<Transform> Lines;

    private void Awake()
    {
        for (int i = 0; i < checkPoints.Count - 1; i++)
        {
            GameObject obj = Instantiate(LineObject, LineParent);
            Lines.Add(obj.transform);
        }

        for (int i = 0; i < checkPoints.Count; i++)
        {
            Transform[] checkpointInObj = checkPoints[i].GetComponentsInChildren<Transform>();
            List<Transform> tl = new List<Transform>(checkpointInObj);
            llt.Add(tl);
            checkpointInObj[3].gameObject.SetActive(false);
        }

        for (int i = 1; i < llt[nowIdx].Count; i++)
        {
            llt[nowIdx][i].gameObject.SetActive(false);
        }
        llt[nowIdx][3].gameObject.SetActive(true);

    }
    private void init()
    {
        for (int i = 0; i < checkPoints.Count; i++)
        {
            RectTransform rt = checkPoints[i].GetComponent<RectTransform>();

            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, Random.Range(-70, 70));
            //checkPoints[i].transform.position = new Vector3(checkPoints[i].transform.position.x, Random.Range(-130, 130), checkPoints[i].transform.position.z);
        }
        nav_chartCourse_ship.GetComponent<RectTransform>().anchoredPosition = checkPoints[0].GetComponent<RectTransform>().anchoredPosition;



        // 라인 생성
        CreateLine();
    }

    private void OnEnable()
    {
        init();
    }

    void Start()
    {
        //init();
    }

    // Update is called once per frame
    void Update()
    {
        if (isClear)
        {
            return;
        }
        ChartCourseMissionFunction();
    }

    private void ChartCourseMissionFunction()
    {
        if (nowIdx < checkPoints.Count-1)
        {
            nav_chartCourse_ship.up = checkPoints[nowIdx + 1].transform.position - checkPoints[nowIdx].transform.position;

            if (Input.GetMouseButton(0))
            {
                float px = Mathf.Lerp(checkPoints[nowIdx].transform.position.x, checkPoints[nowIdx + 1].transform.position.x, (Input.mousePosition.x - checkPoints[nowIdx].transform.position.x) / (checkPoints[nowIdx + 1].transform.position.x - checkPoints[nowIdx].transform.position.x));
                float py = Mathf.Lerp(checkPoints[nowIdx].transform.position.y, checkPoints[nowIdx + 1].transform.position.y, (Input.mousePosition.y - checkPoints[nowIdx].transform.position.y) / (checkPoints[nowIdx + 1].transform.position.y - checkPoints[nowIdx].transform.position.y));

                Vector3 pos = Vector3.Lerp(checkPoints[nowIdx].transform.position, checkPoints[nowIdx + 1].transform.position, (Input.mousePosition.x - checkPoints[nowIdx].transform.position.x) / (checkPoints[nowIdx + 1].transform.position.x - checkPoints[nowIdx].transform.position.x));
                if (Input.mousePosition.x <= px + range && Input.mousePosition.x >= px - range
                    && Input.mousePosition.y <= py + range && Input.mousePosition.y >= py - range)
                {
                    nav_chartCourse_ship.transform.position = pos;
                }
                else
                {
                    nav_chartCourse_ship.transform.position = Vector3.MoveTowards(nav_chartCourse_ship.transform.position, checkPoints[nowIdx].transform.position, Time.deltaTime * 100);
                }

                if (nav_chartCourse_ship.transform.position.x == checkPoints[nowIdx + 1].transform.position.x &&
                    nav_chartCourse_ship.transform.position.y == checkPoints[nowIdx + 1].transform.position.y)
                {
                    nowIdx++;
                    var checkpointInObj = checkPoints[nowIdx].GetComponentsInChildren<Transform>();
                    for (int i = 1; i < llt[nowIdx].Count; i++)
                    {
                        llt[nowIdx][i].gameObject.SetActive(false);
                    }
                    llt[nowIdx][3].gameObject.SetActive(true);
                }
            }
            else
            {
                nav_chartCourse_ship.transform.position = Vector3.MoveTowards(nav_chartCourse_ship.transform.position, checkPoints[nowIdx].transform.position, Time.deltaTime * 100);
            }
        }
        else
        {
            isClear = true;
            Invoke("CloseUI", 1.0f);
        }
    }
    private void CloseUI()
    {
        gameObject.SetActive(false);
    }


    private void CreateLine()
    {
        float lineHeight = LineObject.GetComponent<RectTransform>().rect.height;
        for (int i = 0; i < checkPoints.Count-1; i++)
        {
            float dist = Vector3.Distance(checkPoints[i + 1].transform.position, checkPoints[i].transform.position);
            float lineCount = dist / lineHeight;
            
            Vector3 direction = checkPoints[i + 1].transform.position - checkPoints[i].transform.position;
            Lines[i].transform.up = direction;
            ImageScroll thisLineScrpit = Lines[i].GetComponent<ImageScroll>();
            thisLineScrpit.DiffuseTillingYSize = lineCount;
            thisLineScrpit.RectHeight = dist;
            thisLineScrpit.transform.position = checkPoints[i].transform.position;
        }
    }

}
