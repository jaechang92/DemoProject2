using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EjectionUI : MonoBehaviour
{
    [SerializeField]
    private Text ejectionResultText;
    [SerializeField]
    private Image ejectionPlayer;
    [SerializeField]
    private RectTransform left;
    [SerializeField]
    private RectTransform right;



    void Start()
    {
        ejectionPlayer.material = Instantiate(ejectionPlayer.material);


    }

    public void Open(bool isEjection,EPlayerColor ejectionPlayerColor, bool isImposter, int remainImposterCount)
    {
        string text = "";
        InGameCharacterMover ejectPlayer = null;
        if (isEjection)
        {
            var players = FindObjectsOfType<InGameCharacterMover>();
            foreach (var player in players)
            {
                if (player.playerColor == ejectionPlayerColor)
                {
                    ejectPlayer = player;
                    break;
                }
            }
            text = string.Format("{0}은 임포스터{1}\n 임포스터가 {2}명 남았습니다.", ejectPlayer.nickname, isImposter ? "입니다." : "가 아닙니다.", remainImposterCount);
        }
        else
        {
            text = string.Format("아무도 퇴출되지 않았습니다.\n 임포스터가 {0}명 남았습니다.", remainImposterCount);
        }

        gameObject.SetActive(true);
        StartCoroutine(ShowEjectionResult_Coroutine(ejectPlayer,text));
    }

    private IEnumerator ShowEjectionResult_Coroutine(InGameCharacterMover ejectionPlayerMover, string text  )
    {
        ejectionResultText.text = "";

        string forwardText = "";
        string backwardText = "";
        if (ejectionPlayerMover != null)
        {
            ejectionPlayer.material.SetColor("_PlayerColor", PlayerColor.GetColor(ejectionPlayerMover.playerColor));
            float timer = 0f;
            while (timer <= 1f)
            {
                yield return null;
                timer += Time.deltaTime * 0.5f;
                ejectionPlayer.rectTransform.anchoredPosition = Vector2.Lerp(left.anchoredPosition, right.anchoredPosition, timer);
                ejectionPlayer.rectTransform.rotation = Quaternion.Euler(ejectionPlayer.rectTransform.rotation.eulerAngles + new Vector3(0f, 0f, -360f * Time.deltaTime));
            }
        }
        backwardText = text;
        while (backwardText.Length != 0)
        {
            forwardText += backwardText[0];
            backwardText = backwardText.Remove(0, 1);
            ejectionResultText.text = string.Format("<color=#FFFFFF>{0}</color><color=#000000>{1}</color>", forwardText, backwardText);
            yield return new WaitForSeconds(0.1f);
        }

    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
