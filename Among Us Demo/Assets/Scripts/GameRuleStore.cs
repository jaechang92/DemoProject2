using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Text;

public enum EKillRange
{
    Short,
    Normal,
    Long,
    Max
}

public enum ETaskBarUpdates
{
    Always,
    Meetings,
    Never,
    Max
}

public struct GameRuleData
{
    public bool confirmEjects;
    public int emergencyMeetings;
    public int emergencyMeetingsCooldown;
    public int meetingTime;
    public int voteTime;
    public bool anonymousVotes;
    public float moveSpeed;
    public float crewSight;
    public float imposterSight;
    public float killCooldown;
    public EKillRange killRange;
    public bool visualTasks;
    public ETaskBarUpdates taskBarUpdates;
    public int commonTask;
    public int complexTask;
    public int simpleTask;
}

public class GameRuleStore : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetIsRecommendRule_Hook))]
    private bool isRecommendRule;
    [SerializeField]
    private Toggle isRecommendRuleToggle;

    public void SetIsRecommendRule_Hook(bool _, bool value)
    {
        UpdateGameRuleOverview();
    }
    public void OnReCommendToggle(bool value)
    {
        isRecommendRule = value;
        if (isRecommendRule)
        {
            SetRecommendGameRule();
        }
    }

    


    [SyncVar(hook = nameof(SetConfirmEjectsToggle_Hook))]
    private bool confirmEjects;
    [SerializeField]
    private Toggle confirmEjectsToggle;
    public void SetConfirmEjectsToggle_Hook(bool _, bool value)
    {
        UpdateGameRuleOverview();
    }
    public void OnComfirmEjectsToggle(bool value)
    {
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
        confirmEjects = value;
    }

    [SyncVar(hook = nameof(SetEmergencyMeetingsText_Hook))]
    private int emergencyMeetings;
    [SerializeField]
    private Text emergencyMeetingsText;
    public void SetEmergencyMeetingsText_Hook(int _, int value)
    {
        emergencyMeetingsText.text = value.ToString();
        UpdateGameRuleOverview();
    }
    //public void OnChange(bool isPlus)
    //{
    //     = Mathf.Clamp(+(isPlus ? 1 : -1), 0, 9);
    //    isRecommendRule = false;
    //    isRecommendRuleToggle.isOn = false;
    //}

    public void OnChangeEmergencyMeetings(bool isPlus)
    {
        emergencyMeetings = Mathf.Clamp(emergencyMeetings + (isPlus ? 1 : -1), 0, 9);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
    }


    [SyncVar(hook = nameof(SetEmergencyMeetingsCooldownText_Hook))]
    private int emergencyMeetingsCooldown;
    [SerializeField]
    private Text emergencyMeetingsCooldownText;
    public void SetEmergencyMeetingsCooldownText_Hook(int _, int value)
    {
        emergencyMeetingsCooldownText.text = string.Format("{0}s", emergencyMeetingsCooldown);
        UpdateGameRuleOverview();
    }
    public void OnChangeEmergencyMeetingsCooldown(bool isPlus)
    {
        emergencyMeetingsCooldown = Mathf.Clamp(emergencyMeetingsCooldown + (isPlus ? 5 : -5), 0, 60);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetMeetingTimeText_Hook))]
    private int meetingTime;
    [SerializeField]
    private Text meetingTimeText;
    public void SetMeetingTimeText_Hook(int _, int value)
    {
        meetingTimeText.text = string.Format("{0}s", meetingTime);
        UpdateGameRuleOverview();
    }
    public void OnChangeMeetingTime(bool isPlus)
    {
        meetingTime = Mathf.Clamp(meetingTime + (isPlus ? 5 : -5), 0, 120);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetVoteTimeText_Hook))]
    private int voteTime;
    [SerializeField]
    private Text voteTimeText;
    public void SetVoteTimeText_Hook(int _, int value)
    {
        voteTimeText.text = string.Format("{0}s", voteTime);
        UpdateGameRuleOverview();
    }
    public void OnChangeVoteTime(bool isPlus)
    {
        voteTime = Mathf.Clamp(voteTime + (isPlus ? 5 : -5), 0, 300);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetAnonymousVotesToggle_Hook))]
    private bool anonymousVotes;
    [SerializeField]
    private Toggle anonymousVotesToggle;
    public void SetAnonymousVotesToggle_Hook(bool _, bool value)
    {
        UpdateGameRuleOverview();
    }
    public void OnAnonymousVotesToggle(bool value)
    {
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
        anonymousVotes = value;
    }

    [SyncVar(hook = nameof(SetMoveSpeedText_Hook))]
    private float moveSpeed;
    [SerializeField]
    private Text moveSpeedText;
    public void SetMoveSpeedText_Hook(float _, float value)
    {
        moveSpeedText.text = string.Format("{0:0.0}x", moveSpeed);
        UpdateGameRuleOverview();
    }
    public void OnChangeMoveSpeed(bool isPlus)
    {
        moveSpeed = Mathf.Clamp(moveSpeed + (isPlus ? 0.25f : -0.25f), 0.5f, 3f);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
        var manager = NetworkManager.singleton as AmongUsRoomManager;
        for (int i = 0; i < manager.roomSlots.Count; i++)
        {
            manager.roomSlots[i].GetComponent<AmongUsRoomPlayer>().myCharacter.moveSpeed = moveSpeed;
        }
    }

    [SyncVar(hook = nameof(SetCrewSightText_Hook))]
    private float crewSight;
    [SerializeField]
    private Text crewSightText;
    public void SetCrewSightText_Hook(float _, float value)
    {
        crewSightText.text = string.Format("{0:0.0}x", crewSight);
        UpdateGameRuleOverview();
    }
    public void OnChangeCrewSight(bool isPlus)
    {
        crewSight = Mathf.Clamp(crewSight + (isPlus ? 0.25f : -0.25f), 0.25f, 5f);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetImposterSightText_Hook))]
    private float imposterSight;
    [SerializeField]
    private Text imposterSightText;
    public void SetImposterSightText_Hook(float _, float value)
    {
        imposterSightText.text = string.Format("{0:0.0}x", imposterSight);
        UpdateGameRuleOverview();
    }
    public void OnChangeImposterSight(bool isPlus)
    {
        imposterSight = Mathf.Clamp(imposterSight + (isPlus ? 0.25f : -0.25f), 0.25f, 5f);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetKillCooldownText_Hook))]
    private float killCooldown;
    [SerializeField]
    private Text killCooldownText;
    public void SetKillCooldownText_Hook(float _, float value)
    {
        killCooldownText.text = string.Format("{0:0.0}s", killCooldown);
        UpdateGameRuleOverview();
    }
    public void OnChangeKillCooldown(bool isPlus)
    {
        killCooldown = Mathf.Clamp(killCooldown + (isPlus ? 2.5f : -2.5f), 10f, 60f);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetKillRangeText_Hook))]
    private EKillRange killRange;
    [SerializeField]
    private Text killRangeText;
    public void SetKillRangeText_Hook(EKillRange _, EKillRange value)
    {
        killRangeText.text = value.ToString();
        UpdateGameRuleOverview();
    }
    public void OnChangeKillRange(bool isPlus)
    {
        killRange = (EKillRange)Mathf.Clamp((int)killRange + (isPlus ? 1 : -1), 0, 2);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetVisualTasksToggle_Hook))]
    private bool visualTasks;
    [SerializeField]
    private Toggle visualTasksToggle;
    public void SetVisualTasksToggle_Hook(bool _, bool value)
    {
        UpdateGameRuleOverview();
    }
    public void OnVisualTasksToggle(bool value)
    {
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
        visualTasks = value;
    }

    [SyncVar(hook = nameof(SetTaskBarUpdatesText_Hook))]
    private ETaskBarUpdates taskBarUpdates;
    [SerializeField]
    private Text taskBarUpdatesText;
    public void SetTaskBarUpdatesText_Hook(ETaskBarUpdates _, ETaskBarUpdates value)
    {
        taskBarUpdatesText.text = value.ToString();
        UpdateGameRuleOverview();
    }
    public void OnChangeTaskBarUpdates(bool isPlus)
    {
        taskBarUpdates = (ETaskBarUpdates)Mathf.Clamp((int)taskBarUpdates + (isPlus ? 1 : -1), 0, 2);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetCommonTaskText_Hook))]
    private int commonTask;
    [SerializeField]
    private Text commonTaskText;
    public void SetCommonTaskText_Hook(int _, int value)
    {
        commonTaskText.text = value.ToString();
        UpdateGameRuleOverview();
    }
    public void OnChangeCommonTask(bool isPlus)
    {
        commonTask = Mathf.Clamp(commonTask + (isPlus ? 1 : -1), 0, 5);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetComplexTaskText_Hook))]
    private int complexTask;
    [SerializeField]
    private Text complexTaskText;
    public void SetComplexTaskText_Hook(int _, int value)
    {
        complexTaskText.text = value.ToString();
        UpdateGameRuleOverview();
    }
    public void OnChangeComplexTask(bool isPlus)
    {
        complexTask = Mathf.Clamp(complexTask + (isPlus ? 1 : -1), 0, 5);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetSimpleTaskText_Hook))]
    private int simpleTask;
    [SerializeField]
    private Text simpleTaskText;
    public void SetSimpleTaskText_Hook(int _, int value)
    {
        simpleTaskText.text = value.ToString();
        UpdateGameRuleOverview();
    }
    public void OnChangeSimpleTask(bool isPlus)
    {
        simpleTask = Mathf.Clamp(simpleTask + (isPlus ? 1 : -1), 0, 7);
        isRecommendRule = false;
        isRecommendRuleToggle.isOn = false;
    }

    [SyncVar(hook = nameof(SetImposterCount_Hook))]
    private int imposterCount;
    public void SetImposterCount_Hook(int _, int value)
    {
        UpdateGameRuleOverview();
    }

    [SerializeField]
    private Text gameRuleOverview;

    public void UpdateGameRuleOverview()
    {
        
        StringBuilder sb = new StringBuilder(isRecommendRule ? "?????? ??????\n" : "????????? ??????\n");
        sb.Append("???: The Skeld\n");
        sb.Append($"#????????????: {imposterCount}\n");
        sb.Append(string.Format("Confirm Ejects: {0}\n", confirmEjects ? "??????" : "??????"));
        sb.Append($"?????? ??????: {emergencyMeetings}\n");
        sb.Append(string.Format("Anonymous Votes: {0}\n", anonymousVotes ? "??????" : "??????"));
        sb.Append($"?????? ?????? ?????????: {emergencyMeetingsCooldown}\n");
        sb.Append($"?????? ?????? ??????: {meetingTime}\n");
        sb.Append($"?????? ?????? ??????: {voteTime}\n");
        sb.Append($"?????? ??????: {moveSpeed}\n");
        sb.Append($"????????? ??????: {crewSight}\n");
        sb.Append($"???????????? ??????: {imposterSight}\n");
        sb.Append($"??? ?????????: {killCooldown}\n");
        sb.Append($"??? ??????: {killRange}\n");
        sb.Append($"Task Bar Updates: {taskBarUpdates}\n");
        sb.Append(string.Format("Visual Task: {0}\n", visualTasks ? "??????" : "??????"));
        sb.Append($"?????? ??????: {commonTask}\n");
        sb.Append($"????????? ??????: {complexTask}\n");
        sb.Append($"????????? ??????: {simpleTask}\n");
        gameRuleOverview.text = sb.ToString();

    }

    private void SetRecommendGameRule()
    {
        isRecommendRule = true;
        confirmEjects = true;
        emergencyMeetings = 1;
        emergencyMeetingsCooldown = 15;
        meetingTime = 15;
        voteTime = 120;
        moveSpeed = 1.0f;
        crewSight = 1.0f;
        imposterSight = 1.5f;
        killCooldown = 45f;
        killRange = EKillRange.Normal;
        visualTasks = true;
        commonTask = 1;
        complexTask = 2;
        simpleTask = 2;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            var manager = NetworkManager.singleton as AmongUsRoomManager;
            imposterCount = manager.imposterCount;
            anonymousVotes = false;
            taskBarUpdates = ETaskBarUpdates.Always;

            SetRecommendGameRule();
        }
        
    }

    public GameRuleData GetGameRuleData()
    {
        return new GameRuleData()
        {
            anonymousVotes = anonymousVotes,
            commonTask = commonTask,
            complexTask = complexTask,
            confirmEjects = confirmEjects,
            crewSight = crewSight,
            emergencyMeetings = emergencyMeetings,
            emergencyMeetingsCooldown = emergencyMeetingsCooldown,
            imposterSight = imposterSight,
            killCooldown = killCooldown,
            killRange = killRange,
            meetingTime = meetingTime,
            moveSpeed = moveSpeed,
            simpleTask = simpleTask,
            taskBarUpdates = taskBarUpdates,
            visualTasks = visualTasks,
            voteTime = voteTime,
        };
    }
}
