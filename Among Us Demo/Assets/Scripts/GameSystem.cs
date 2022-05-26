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

    [SyncVar]
    public int skipVotePlayerCount;
    [SyncVar]
    public int voteCount;

    [SyncVar]
    public float remainTime;

    [SyncVar]
    public int sabotageCheckCount = 0;



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

        AllocatePlayerToAroundTable(players.ToArray());

        yield return new WaitForSeconds(2.0f);
        RpcStartGame();

        foreach (var player in players)
        {
            player.SetKillCooldown();
        }
    }

    private void AllocatePlayerToAroundTable(InGameCharacterMover[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
            float radian = (2f * Mathf.PI) / players.Length;
            radian *= i;
            players[i].RpcTeleport(spawnTransform.position + (new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0f) * spawnDistance));
            players[i].IsMoveable = false;
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
    public void StartSabotage(E_Sabotage sabotage)
    {
        RpcSendSabotage(sabotage);
        //StartCoroutine(StartSabotage_Coroutine(sabotage));
    }

    public void StartReportMeeting(EPlayerColor deadbodyColor)
    {
        RpcSendReportSign(deadbodyColor);
        StartCoroutine(MeetingProcess_Coroutine());
    }

    private IEnumerator StartMeeting_Coroutine()
    {
        yield return new WaitForSeconds(3.0f);
        InGameUIManager.Instance.ReportUI.Close();
        InGameUIManager.Instance.MeetingUI.Open();
        InGameUIManager.Instance.MeetingUI.ChangeMeetingState(EMeetingState.Meeting);
    }

    private IEnumerator SabotageRedBG_Coroutine()
    {
        while (true)
        {
            if (InGameUIManager.Instance.SabotageRedBG.color.a == 0.15f)
            {
                InGameUIManager.Instance.SabotageRedBG.color = new Color(1, 0, 0, 0);
            }
            else
            {
                InGameUIManager.Instance.SabotageRedBG.color = new Color(1, 0, 0, 0.15f);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
    private void InitSabo()
    {
        InGameUIManager.Instance.SabotageRedBG.gameObject.SetActive(true);
        sabotageCheckCount = 0;
    }

    private IEnumerator StartSabotage_Coroutine(E_Sabotage sabotage)
    {
        switch (sabotage)
        {
            case E_Sabotage.sabotage_reactor:
                // 경고음 + 원자로 융해 사보타지
                // 타이머 온
                Debug.Log("원자로 사보타지");
                InitSabo();
                StartCoroutine(SabotageRedBG_Coroutine());
                break;
            case E_Sabotage.sabotage_O2:
                // 경고음 + 산소 사보타지
                // 타이머 온
                Debug.Log("산소 사보타지");
                InitSabo();
                 StartCoroutine(SabotageRedBG_Coroutine());
                break;
            case E_Sabotage.sabotage_electricity:
                // 일정 시간에 걸쳐 시야 감소
                Debug.Log("전기실 사보타지");

                break;
            case E_Sabotage.sabotage_comms:
                // 미정

                break;
            case E_Sabotage.sabotage_Doors:
                // 미정

                break;
            default:
                break;
        }
        yield return null;
    }

    private IEnumerator MeetingProcess_Coroutine()
    {
        var players = FindObjectsOfType<InGameCharacterMover>();
        int alivePlayerCount = 0;
        voteCount = 0;
        foreach (var player in players)
        {
            if ((player.playerType & EPlayerType.Ghost) != EPlayerType.Ghost)
            {
                alivePlayerCount++;
            }
            player.isVote = true;
        }
        yield return new WaitForSeconds(3f);

        var manager = NetworkManager.singleton as AmongUsRoomManager;
        remainTime = manager.gameRuleData.meetingTime;
        while (true)
        {
            remainTime -= Time.deltaTime;
            yield return null;
            if (remainTime <= 0f)
            {
                break;
            }
        }

        skipVotePlayerCount = 0;
        foreach (var player in players)
        {
            if ((player.playerType & EPlayerType.Ghost) != EPlayerType.Ghost)
            {
                player.isVote = false;
            }
            player.vote = 0;
        }

        RpcStartVoteTime();
        remainTime = manager.gameRuleData.voteTime;
        while (true)
        {
            remainTime -= Time.deltaTime;
            yield return null;
            if (remainTime <= 0f)
            {
                break;
            }

            if (alivePlayerCount == voteCount + skipVotePlayerCount)
            {
                remainTime = 0;
            }
        }

        foreach (var player in players)
        {
            if (!player.isVote && (player.playerType & EPlayerType.Ghost)!= EPlayerType.Ghost)
            {
                player.isVote = true;
                skipVotePlayerCount += 1;
                RpcSignSkipVote(player.playerColor);
            }
        }

        RpcEndVoteTime();

        yield return new WaitForSeconds(3f);

        StartCoroutine(CalculateVoteResult_Coroutine(players));
    }

    private class CharacterVoteComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            InGameCharacterMover xPlayer = (InGameCharacterMover)x;
            InGameCharacterMover yPlayer = (InGameCharacterMover)y;
            return xPlayer.vote <= yPlayer.vote ? 1 : -1;
        }
    }


    private IEnumerator CalculateVoteResult_Coroutine(InGameCharacterMover[] players)
    {
        System.Array.Sort(players, new CharacterVoteComparer());
        int remainImposter = 0;
        foreach (var player in players)
        {
            if ((player.playerType & EPlayerType.Imposter_Alive) == EPlayerType.Imposter_Alive)
            {
                remainImposter++;
            }
        }

        if (skipVotePlayerCount >= players[0].vote)
        {
            RpcOpenEjectionUI(false, EPlayerColor.Black, false, remainImposter);
        }
        else if(players[0].vote == players[1].vote)
        {
            RpcOpenEjectionUI(false, EPlayerColor.Black, false, remainImposter);
        }
        else
        {
            bool isImposter = (players[0].playerType & EPlayerType.Imposter) == EPlayerType.Imposter;
            RpcOpenEjectionUI(true, players[0].playerColor, isImposter, isImposter ? remainImposter - 1 : remainImposter);

            players[0].Dead(true);
        }

        var deadbodies = FindObjectsOfType<Deadbody>();
        for (int i = 0; i < deadbodies.Length; i++)
        {
            Destroy(deadbodies[i].gameObject);
        }

        AllocatePlayerToAroundTable(players);

        yield return new WaitForSeconds(10f);
        RpcCloseEjectionUI();
    }

    [ClientRpc]
    public void RpcOpenEjectionUI(bool isEjection,EPlayerColor ejectionPlayerColor, bool isImposter,int remainImposterCount)
    {
        InGameUIManager.Instance.EjectionUI.Open(isEjection, ejectionPlayerColor, isImposter, remainImposterCount);
        InGameUIManager.Instance.MeetingUI.Close();
    }

    [ClientRpc]
    public void RpcCloseEjectionUI()
    {
        InGameUIManager.Instance.EjectionUI.Close();
        AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = true;
    }


    [ClientRpc]
    public void RpcStartVoteTime()
    {
        InGameUIManager.Instance.MeetingUI.ChangeMeetingState(EMeetingState.Vote);
    }
    [ClientRpc]
    public void RpcEndVoteTime()
    {
        InGameUIManager.Instance.MeetingUI.CompleteVote();
    }


    [ClientRpc]
    public void RpcSendReportSign(EPlayerColor deadbodyColor)
    {
        InGameUIManager.Instance.ReportUI.Open(deadbodyColor);

        StartCoroutine(StartMeeting_Coroutine());

    }
    [ClientRpc]
    public void RpcSendSabotage(E_Sabotage sabotage)
    {
        StartCoroutine(StartSabotage_Coroutine(sabotage));
    }

    [ClientRpc]
    public void RpcSignVoteEject(EPlayerColor voteColor, EPlayerColor ejectColor)
    {
        InGameUIManager.Instance.MeetingUI.UpdateVote(voteColor, ejectColor);
    }

    [ClientRpc]
    public void RpcSignSkipVote(EPlayerColor skipVotePlayerColor)
    {
        InGameUIManager.Instance.MeetingUI.UpdateSkipVotePlayer(skipVotePlayerColor);
    }


    [ClientRpc]
    public void RpcSabotageCheckFunc(int count)
    {
        if (count >= 2)
        {
            StopAllCoroutines();
            InGameUIManager.Instance.SabotageRedBG.gameObject.SetActive(false);
            sabotageCheckCount = 0;
        }
    }
}
