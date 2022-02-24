using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance;

    [SerializeField]
    private InGameIntroUI inGameIntroUI;
    public InGameIntroUI InGameIntroUI
    {
        get { return inGameIntroUI; }
    }

    [SerializeField]
    private KillButtonUI killButtonUI;
    public KillButtonUI KillButtonUI
    {
        get { return killButtonUI; }
    }


    [SerializeField]
    private KillUI killUI;
    public KillUI KillUI
    {
        get { return killUI; }
    }

    [SerializeField]
    private ReportButtonUI reportButtonUI;
    public ReportButtonUI ReportButtonUI
    {
        get { return reportButtonUI; }
    }

    [SerializeField]
    private ReportUI reportUI;
    public ReportUI ReportUI
    {
        get { return reportUI; }
    }

    [SerializeField]
    private MeetingUI meetingUI;
    public MeetingUI MeetingUI { get { return meetingUI; } }

    [SerializeField]
    private EjectionUI ejectionUI;
    public EjectionUI EjectionUI { get { return ejectionUI; } }

    private void Awake()
    {
        Instance = this;
    }

}
