using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AmongUsRoomPlayer : NetworkRoomPlayer
{
    private static AmongUsRoomPlayer myRoomPlayer;

    public static AmongUsRoomPlayer MyRoomPlayer 
    {
        get 
        {
            if (myRoomPlayer == null)
            {
                var players = FindObjectsOfType<AmongUsRoomPlayer>();
                foreach (var player in players)
                {
                    if (player.hasAuthority)
                    {
                        myRoomPlayer = player;
                    }
                }
            }
            return myRoomPlayer;
        }
    }
    


    [SyncVar(hook = nameof(SetPlayerColor_Hook))]
    public EPlayerColor playerColor;

    public void SetPlayerColor_Hook(EPlayerColor oldColor, EPlayerColor newColor)
    {
        LobbyUIManager.Instance.CustomizeUI.UpdateSelectColorButton(oldColor,newColor);
    }

    [SyncVar]
    public string nickname;

    public CharacterMover myCharacter;


    private void Start()
    {
        base.Start();
        if (isServer)
        {
            SpawnLobbyPlayerCharacter();
            LobbyUIManager.Instance.ActiveStartButton();
        }

        if (isLocalPlayer)
        {
            CmdSetNickname(PlayerSettins.nickName);
        }

        LobbyUIManager.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
    }

    private void OnDestroy()
    {
        if (LobbyUIManager.Instance != null)
        {
            LobbyUIManager.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
            LobbyUIManager.Instance.CustomizeUI.UpdateUnSelectColorButton(playerColor);
        }
    }

    [Command]
    public void CmdSetNickname(string nick)
    {
        nickname = nick;
        myCharacter.nickname = nick;
    }

    private void SpawnLobbyPlayerCharacter()
    {
        var roomSlots = (NetworkManager.singleton as AmongUsRoomManager).roomSlots;
        EPlayerColor color = EPlayerColor.Red;
        for (int i = 0; i < (int)EPlayerColor.Max; i++)
        {
            bool isFindSameColor = false;
            foreach (var roomPlayer in roomSlots)
            {
                var amongUsRoomPlayer = roomPlayer as AmongUsRoomPlayer;
                if (amongUsRoomPlayer.playerColor == (EPlayerColor)i && roomPlayer.netId != netId)
                {
                    isFindSameColor = true;
                    break;
                }
            }

            if (!isFindSameColor)
            {
                color = (EPlayerColor)i;
                break;
            }
        }
        playerColor = color;

        var spawnPosition = FindObjectOfType<SpawnPosition>();
        Vector3 spawnPos = spawnPosition.GetSpawnPosition();
        int indexPos = spawnPosition.Index;
        Debug.Log(indexPos);
        LobbyCharacterMover playerCharacter = Instantiate(AmongUsRoomManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).GetComponent<LobbyCharacterMover>();
        if (indexPos > 5)
        {
            playerCharacter.transform.localScale = new Vector3(-Mathf.Abs(playerCharacter.transform.localScale.x), playerCharacter.transform.localScale.y, playerCharacter.transform.localScale.z);
        }
        NetworkServer.Spawn(playerCharacter.gameObject, connectionToClient);
        playerCharacter.ownerNetId = netId;
        playerCharacter.playerColor = color;
    }

    

    [Command]
    public void CmdSetPlayerColor(EPlayerColor color)
    {
        playerColor = color;
        myCharacter.playerColor = color;
    }

}
