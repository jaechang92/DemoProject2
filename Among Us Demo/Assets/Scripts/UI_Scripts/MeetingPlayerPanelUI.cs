using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeetingPlayerPanelUI : MonoBehaviour
{
    [SerializeField]
    private Image characterImage;
    [SerializeField]
    private Text nicknameText;
    [SerializeField]
    private GameObject deadPlayerBlock;
    [SerializeField]
    private GameObject reportSign;
    [SerializeField]
    private GameObject voteButtons;


    [HideInInspector]
    public InGameCharacterMover targetPlayer;


    public void SetPlayer(InGameCharacterMover target)
    {
        Material inst = Instantiate(characterImage.material);
        characterImage.material = inst;

        targetPlayer = target;
        characterImage.material.SetColor("_PlayerColor", PlayerColor.GetColor(targetPlayer.playerColor));
        nicknameText.text = targetPlayer.nickname;


        var myCharacter = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as InGameCharacterMover;
        if (((myCharacter.playerType & EPlayerType.Imposter) == EPlayerType.Imposter) && ((targetPlayer.playerType & EPlayerType.Imposter) == EPlayerType.Imposter))
        {
            nicknameText.color = Color.red;
        }

        bool isDead = (targetPlayer.playerType & EPlayerType.Ghost) == EPlayerType.Ghost;
        deadPlayerBlock.SetActive(isDead);
        GetComponent<Button>().interactable = !isDead;

        reportSign.SetActive(targetPlayer.isReporter);

    }

    public void OnClickPlayerPanel()
    {
        var myCharacter = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as InGameCharacterMover;
        if ((myCharacter.playerType & EPlayerType.Ghost) != EPlayerType.Ghost)
        {
            InGameUIManager.Instance.MeetingUI.SelectPlayerPanel();
            voteButtons.SetActive(true);
        }

    }

    public void Unselect()
    {
        voteButtons.SetActive(false);


    }

}
