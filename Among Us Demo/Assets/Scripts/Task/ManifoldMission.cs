using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ManifoldMission : MonoBehaviour
{

    [SerializeField]
    private List<Image> numberImageList;
    [SerializeField]
    private List<Sprite> numberSpriteList;

    [SerializeField]
    private bool isClear = false;

    [SerializeField]
    private Color colorCode;

    void Start()
    {
        int numberIdx = 0;
        while (numberSpriteList.Count != 0)
        {
            int idx = Random.Range(0, numberSpriteList.Count);
            numberImageList[numberIdx].sprite = numberSpriteList[idx];
            numberSpriteList.Remove(numberSpriteList[idx]);
            numberIdx++;
        }
    }

    int missionNumber = 1;
    public void ClickNumber()
    {
        string numberFormat = missionNumber.ToString("0#");
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name == "reactorButton" + numberFormat)
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = colorCode;
            missionNumber++;
        }

        if (missionNumber == 11)
        {
            isClear = true;

            Invoke("CloseUI", 1.0f);
        }

    }

    private void CloseUI()
    {
        gameObject.SetActive(false);
    }

}
