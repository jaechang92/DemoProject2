using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance;
    public Text debugText;

    public string MyDebuger(Object bValue)
    {
        return bValue.GetType().Name + bValue == null ? "= null" : "!= null" + "\n";
    }
    public string MyDebuger(string str)
    {
        return str + "\n";
    }



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
    private GameObject sabotageButtonUI;
    public GameObject SabotageButtonUI
    {
        get { return sabotageButtonUI; }
    }

    [SerializeField]
    private UseButtonUI useButtonUI;
    public UseButtonUI UseButtonUI
    {
        get { return useButtonUI; }
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

    [SerializeField]
    private GameObject taskUI;
    public GameObject TaskUI { get { return taskUI; } }

    [SerializeField]
    private Image tasksProgress;
    public Image TasksProgress { get { return tasksProgress; } }



    [SerializeField]
    private Sprite originUseButtonSprite;

    [SerializeField]
    private Image sabotageRedBG;
    public Image SabotageRedBG { get { return sabotageRedBG; } }
    public void SetUesButton(Sprite sprite)
    {
        originUseButtonSprite = useButtonUI.useButton.image.sprite;

        if (sprite != null)
        {
            useButtonUI.useButton.image.sprite = sprite;
        }
        useButtonUI.useButton.interactable = true;
    }
    public void SetUesButton(UnityAction action)
    {
        useButtonUI.useButton.onClick.RemoveAllListeners();
        useButtonUI.useButton.onClick.AddListener(action);
    }
    public void SetUesButton(Sprite sprite, UnityAction action)
    {
        useButtonUI.useButton.onClick.RemoveAllListeners();
        originUseButtonSprite = useButtonUI.useButton.image.sprite;
        if (sprite != null)
        {
            useButtonUI.useButton.image.sprite = sprite;
        }
        useButtonUI.useButton.onClick.AddListener(action);
        useButtonUI.useButton.interactable = true;
    }

    public void UnSetUesButton()
    {
        useButtonUI.useButton.image.sprite = originUseButtonSprite;
        useButtonUI.useButton.onClick.RemoveAllListeners();
        useButtonUI.useButton.interactable = false;
    }




    private void Awake()
    {
        Instance = this;
        tasksProgress.fillAmount = 0;
    }

    public void CloseTaskUI(GameObject obj,float waitTime)
    {
        TaskManager.instance.clearCount++;
        TaskManager.instance.TaskClear(obj);
        InGameUIManager.Instance.UpdateTasksProgress();
        StartCoroutine(CloseUI(obj, waitTime));
    }
    public IEnumerator CloseUI(GameObject obj, float waitTime)
    {
        TaskManager.instance.TaskObjectCollidersOff();
        yield return new WaitForSeconds(waitTime);
        obj.SetActive(false);
        TaskManager.instance.TaskObject = null;
    }
    public void UpdateTasksProgress()
    {
        tasksProgress.fillAmount = (float)TaskManager.instance.clearCount / TaskManager.instance.taskCount;
    }

    public void ClickSabotage()
    {
        var character = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as InGameCharacterMover;
        SabotageUI saboUI = EventSystem.current.currentSelectedGameObject.GetComponent<SabotageUI>();
        E_Sabotage sabo = saboUI.sabotage;
        for (int i = 0; i < saboUI.sabotageObjects.Count; i++)
        {
            TaskManager.instance.TaskObjectCollidersOn(saboUI.sabotageObjects[i]);
        }

        character.Sabotage(sabo);


    }

    public void PointerDown()
    {
        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable == true)
        {
            AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = false;
        }

    }
    public void PointerUp()
    {
        if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable == true)
        {
            AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = true;
        }
    }

}
