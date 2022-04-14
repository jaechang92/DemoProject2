using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmiteScanMission : MonoBehaviour
{
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private Text infoText;
    [SerializeField]
    private Image gage;
    [SerializeField]
    private float m_time;
    private float originTime;

    [SerializeField]
    private bool isClear = false;
    [SerializeField]
    private string infoTextDetail;
    [SerializeField]
    InGameCharacterMover myPlayer = null;

    private void Awake()
    {
        originTime = m_time;
    }

    private void OnEnable()
    {
        gage.fillAmount = 0;
        timerText.text = string.Format("검사 완료까지 {0:0} 초", m_time);
        infoText.text = "";
        m_time = originTime;

        var players = FindObjectsOfType<InGameCharacterMover>();
        foreach (var player in players)
        {
            if (player.hasAuthority)
            {
                myPlayer = player;
                break;
            }
        }
        if (myPlayer != null)
        {
            infoTextDetail = string.Format("ID: {0}   HT:3'6   WT:92lb\nColor: {1} ", myPlayer.netId, EnumToString(myPlayer.playerColor));
        }
        else
        {
            infoTextDetail = string.Format("ID: {0}   HT:3'6   WT:92lb\nColor: {1} ", "textID", EnumToString(EPlayerColor.Green));
        }

        StartCoroutine(ShowScanResult_Coroutine(infoTextDetail));
    }

    private void Update()
    {
        if (m_time >= 0)
        {
            m_time -= Time.deltaTime;
            timerText.text = string.Format("검사 완료까지 {0:0} 초", m_time);
            gage.fillAmount += Time.deltaTime / originTime;
        }
        else
        {
            isClear = true;
            Invoke("CloseUI", 1.0f);
        }

    }

    private IEnumerator ShowScanResult_Coroutine(string text)
    {

        string forwardText = "";
        string backwardText = "";

        backwardText = text;
        while (backwardText.Length != 0)
        {
            forwardText += backwardText[0];
            backwardText = backwardText.Remove(0, 1);
            infoText.text = string.Format("<color=#FFFFFF>{0}</color>", forwardText);
            yield return new WaitForSeconds(0.1f);
        }

    }

    string EnumToString(EPlayerColor color)
    {
        return color.ToString();
    }


    private void CloseUI()
    {
        gameObject.SetActive(false);
    }

}
