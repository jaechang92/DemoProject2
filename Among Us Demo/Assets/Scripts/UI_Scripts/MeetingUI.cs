using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeetingUI : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPanelPrefab;
    [SerializeField]
    private Transform playerPanelParent;
    
    private List<MeetingPlayerPanelUI> meetingPlayerPanels = new List<MeetingPlayerPanelUI>();

    
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

    public void SelectPlayerPanel()
    {
        foreach (var panel in meetingPlayerPanels)
        {
            panel.Unselect();
        }
    }



}
