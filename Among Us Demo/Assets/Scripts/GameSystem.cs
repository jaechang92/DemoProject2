using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Mirror;

public class GameSystem : NetworkBehaviour
{
    public static GameSystem Instance;

    private List<InGameCharacterMover> players = new List<InGameCharacterMover>();

    [SerializeField]
    private Transform spawnTransform;

    [SerializeField]
    private float spawnDistance;

    [SyncVar]
    public float killCooldown;
    
    [SyncVar]
    public int killRange;

    [SerializeField]
    private Light2D shadowLight;
    [SerializeField]
    private Light2D lightMapLight;
    [SerializeField]
    private Light2D globalLight;


    public void AddPlayer(InGameCharacterMover player)
    {
        if (!players.Contains(player))
        {
            players.Add(player);
        }

    }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (isServer)
        {
            StartCoroutine(GameReady());
        }
    }

    public List<InGameCharacterMover> GetPlayerList()
    {
        return players;
    }


    private IEnumerator GameReady()
    {
        var manager = NetworkManager.singleton as AmongUsRoomManager;
        killCooldown = manager.gameRuleData.killCooldown;
        killRange = (int)manager.gameRuleData.killRange;

        while (manager.roomSlots.Count != players.Count)
        {
            yield return null;
        }


        for (int i = 0; i < manager.imposterCount; i++)
        {
            var player = players[Random.Range(0, players.Count)];
            if (player.playerType != EPlayerType.Imposter)
            {
                player.playerType = EPlayerType.Imposter;
            }
            else
            {
                i--;
            }
        }

        for (int i = 0; i < players.Count; i++)
        {
            float radian = (2f * Mathf.PI) / players.Count;
            radian *= i;
            players[i].RpcTeleport(spawnTransform.position + (new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f) * spawnDistance));
            players[i].IsMoveable = false;
        }


        yield return new WaitForSeconds(2.0f);
        RpcStartGame();

        foreach (var player in players)
        {
            player.SetKillCooldown();
        }
    }

    [ClientRpc]
    private void RpcStartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return StartCoroutine(InGameUIManager.Instance.InGameIntroUI.ShowIntroSequence());
        
        InGameCharacterMover myCharacter = null;
        foreach (var player in players)
        {
            if (player.hasAuthority)
            {
                myCharacter = player;
                break;
            }
        }

        foreach (var player in players)
        {
            player.SetNicknameColor(myCharacter.playerType);
        }


        yield return new WaitForSeconds(3.0f);
        InGameUIManager.Instance.InGameIntroUI.Close();

        for (int i = 0; i < players.Count; i++)
        {
            players[i].IsMoveable = true;
        }
    }

    public void ChangeLightMode(EPlayerType type)
    {
        if (type == EPlayerType.Ghost)
        {
            lightMapLight.lightType = Light2D.LightType.Global;
            shadowLight.intensity = 0f;
            globalLight.intensity = 1f;
        }
        else
        {
            lightMapLight.lightType = Light2D.LightType.Point;
            shadowLight.intensity = 0.5f;
            globalLight.intensity = 0.5f;
        }
    }

    public void StartReportMeeting(EPlayerColor deadbodyColor)
    {
        RpcSendReportSign(deadbodyColor);
    }

    private IEnumerator StartMeeting_Coroutine()
    {
        yield return new WaitForSeconds(3.0f);
        InGameUIManager.Instance.ReportUI.Close();
        InGameUIManager.Instance.MeetingUI.Open();
    }

    [ClientRpc]
    public void RpcSendReportSign(EPlayerColor deadbodyColor)
    {
        InGameUIManager.Instance.ReportUI.Open(deadbodyColor);

        StartCoroutine(StartMeeting_Coroutine());

    }

}
