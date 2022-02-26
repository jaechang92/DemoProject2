using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class WireTaskControl : MonoBehaviour, IPointerDownHandler , IPointerUpHandler
{
    [SerializeField]
    private List<GameObject> leftWireList;
    [SerializeField]
    private List<GameObject> rightWireList;


    [SerializeField]
    private float offset = 15f;

    private LeftWire nowLeftWire;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("task_wire"))
        {
            nowLeftWire = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<LeftWire>();
            nowLeftWire.mSelectedWire = nowLeftWire;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        if (nowLeftWire.GetComponent<Image>().color == eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().color)
        {
            Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
            nowLeftWire.isConnect = true;
            nowLeftWire.target = eventData.pointerCurrentRaycast.gameObject;
        }
    }


    private void Start()
    {
        ColorSetting();
    }
    List<Color> colorList = new List<Color>();
    public void ColorSetting()
    {
        colorList.Add(Color.red);
        colorList.Add(Color.blue);
        colorList.Add(Color.green);
        colorList.Add(Color.yellow);

        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, colorList.Count);
            for (int j = 0; j < 3; j++)
            {
                leftWireList[i].GetComponentInChildren<LeftWire>().changeColorList[j].color = colorList[rand];
            }
            colorList.RemoveAt(rand);
        }

        colorList.Add(Color.red);
        colorList.Add(Color.blue);
        colorList.Add(Color.green);
        colorList.Add(Color.yellow);
        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, colorList.Count);
            rightWireList[i].GetComponent<Image>().color = colorList[rand];
            colorList.RemoveAt(rand);
        }
    }

   
}
