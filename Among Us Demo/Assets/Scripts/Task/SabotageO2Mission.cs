using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SabotageO2Mission : MonoBehaviour
{
    [SerializeField]
    private string missionNumber;

    [SerializeField]
    private string strNum = null;
    [SerializeField]
    private Text keypadNoteText;
    [SerializeField]
    private Text numberScreenText;
    [SerializeField]
    private bool isClear;

    private void Start()
    {
        SetMissionNumber();
    }

    private void OnEnable()
    {
        InitEnable();
    }

    private void InitEnable()
    {
        isClear = false;
        strNum = "";
        numberScreenText.text = strNum;
    }

    public void SetMissionNumber()
    {
        missionNumber = Random.Range(100000, 1000000).ToString();
        keypadNoteText.text = "today's code:\n" + missionNumber;
    }

    public void ClickNumber(string num)
    {
        if (isClear) return;
        strNum += num;
        numberScreenText.text = strNum;

    }

    public void ClickOKBtn()
    {
        if (isClear) return;
        
        if (strNum == missionNumber)
        {
            isClear = true;
            GameSystem.Instance.sabotageCheckCount++;
            StartCoroutine(InGameUIManager.Instance.CloseUI(gameObject, 1.0f));
            var myCharacter = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as InGameCharacterMover;
            myCharacter.CmdSabotageCheckFunc(GameSystem.Instance.sabotageCheckCount);
        }
        else
        {
            ClickResetBtn();
        }
    }
    public void ClickResetBtn()
    {
        if (isClear) return;
        strNum = "";
        numberScreenText.text = strNum;

    }
}
