using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum EMeetingState
{
    None,
    Meeting,
    Vote,

}

public class MeetingUI : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPanelPrefab;
    [SerializeField]
    private Transform playerPanelParent;
    [SerializeField]
    private GameObject voterPrefab;
    [SerializeField]
    private GameObject skipVoteButton;
    [SerializeField]
    private GameObject skipVotePlayers;
    [SerializeField]
    private Transform skipVoteParentTransform;
    [SerializeField]
    private GameObject skipVoteButtons;

    [SerializeField]
    private Text meetingTimeText;

    private EMeetingState meetingState;

    
    private List<MeetingPlayerPanelUI> meetingPlayerPanels = new List<MeetingPlayerPanelUI>();
    private List<Image> skipVoterImages = new List<Image>();


    public void Open()
    {
        var myCharacter = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as InGameCharacterMover;
        var myPanel = Instantiate(playerPanelPrefab, playerPanelParent).GetComponent<MeetingPlayerPanelUI>();
        myPanel.SetPlayer(myCharacter);
        meetingPlayerPanels.Add(myPanel);
        gameObject.SetActive(true);
        
        var players = FindObjectsOfType<InGameCharacterMover>();
        foreach (var player in players)
        {
            if (player != myCharacter)
            {
                var panel = Instantiate(playerPanelPrefab, playerPanelParent).GetComponent<MeetingPlayerPanelUI>();
                panel.SetPlayer(player);
                meetingPlayerPanels.Add(panel);

            }
        }
    }

    public void ChangeMeetingState(EMeetingState state)
    {
        meetingState = state;

    }

    public void SelectPlayerPanel()
    {
        foreach (var panel in meetingPlayerPanels)
        {
            panel.Unselect();
        }
    }

    public void UpdateVote(EPlayerColor voteColor,EPlayerColor ejectColor)
    {
        foreach (var panel in meetingPlayerPanels)
        {
            if (panel.targetPlayer.playerColor == ejectColor)
            {
                panel.UpdatePanel(voteColor);
            }

            if (panel.targetPlayer.playerColor == voteColor)
            {
                panel.UpdateVoteSign(true);
            }
        }
    }


    public void UpdateSkipVotePlayer(EPlayerColor skipVotePlayerColor)
    {
        foreach (var panel in meetingPlayerPanels)
        {
            if (panel.targetPlayer.playerColor == skipVotePlayerColor)
            {
                panel.UpdateVoteSign(true);
            }
        }

        var voter = Instantiate(voterPrefab, skipVoteParentTransform).GetComponent<Image>();
        voter.material = Instantiate(voter.material);
        voter.material.SetColor("_PlayerColor", PlayerColor.GetColor(skipVotePlayerColor));
        skipVoterImages.Add(voter);
        //skipVoteButton.SetActive(false);

    }


    public void OnClickSkipVoteButton()
    {
        var myCharacter = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as InGameCharacterMover;

        if (myCharacter.isVote)
        {
            return;
        }

        if ((myCharacter.playerType & EPlayerType.Ghost) != EPlayerType.Ghost)
        {
            InGameUIManager.Instance.MeetingUI.SelectPlayerPanel();
            skipVoteButtons.SetActive(true);
        }
    }

    public void SkipSelect()
    {
        var myCharacter = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as InGameCharacterMover;
        myCharacter.CmdSkipVote();
        SelectPlayerPanel();
        SkipUnselect();
    }

    public void SkipUnselect()
    {
        skipVoteButtons.SetActive(false);
    }



    public void CompleteVote()
    {
        foreach (var panel in meetingPlayerPanels)
        {
            panel.OpenResult();
        }

        skipVotePlayers.SetActive(true);
        skipVoteButton.SetActive(false);

    }


    private void Update()
    {
        if (meetingState == EMeetingState.Meeting)
        {
            meetingTimeText.text = string.Format("회의시간 : {0}s", (int)Mathf.Clamp(GameSystem.Instance.remainTime, 0f, float.MaxValue));
        }
        else if (meetingState == EMeetingState.Vote)
        {
            meetingTimeText.text = string.Format("투표시간 : {0}s", (int)Mathf.Clamp(GameSystem.Instance.remainTime, 0f, float.MaxValue));
        }
    }


    public void Close()
    {
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        foreach (var item in meetingPlayerPanels)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in skipVoterImages)
        {
            Destroy(item.gameObject);
        }

        meetingPlayerPanels.Clear();
        skipVoterImages.Clear();
        skipVotePlayers.SetActive(false);
        skipVoteButton.SetActive(true);
    }

}
