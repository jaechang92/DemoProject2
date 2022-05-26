using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapUI : MonoBehaviour
{
    [SerializeField]
    private Transform left;
    [SerializeField]
    private Transform right;
    [SerializeField]
    private Transform top;
    [SerializeField]
    private Transform bottom;

    [SerializeField]
    private Image minimapImage;
    [SerializeField]
    private TaskExclamationMark taskExclamationMarkScript;
    [SerializeField]
    private Image minimapPlayerImage;
    [SerializeField]
    private GameObject sabotage;

    private CharacterMover targetPlayer;

    private Vector2 mapArea;
    public Vector2 MapArea 
    {
        get
        {
            if (mapArea.x == 0 || mapArea.y == 0)
            {
                mapArea = new Vector2(Vector3.Distance(left.position, right.position), Vector3.Distance(bottom.position, top.position));
            }
            return mapArea;
        }
    }


    void Start()
    {
        var inst = Instantiate(minimapImage.material);
        minimapImage.material = inst;

        targetPlayer = AmongUsRoomPlayer.MyRoomPlayer.myCharacter;
    }


    void Update()
    {
        
        if (targetPlayer != null)
        {
            Vector2 charPos = new Vector2(Vector3.Distance(left.position, new Vector3(targetPlayer.transform.position.x, 0f, 0f)),
                Vector3.Distance(bottom.position, new Vector3(0f, targetPlayer.transform.position.y, 0f)));

            Vector2 normalPos = new Vector2(charPos.x / MapArea.x, charPos.y / MapArea.y);
            minimapPlayerImage.rectTransform.anchoredPosition = new Vector2(minimapImage.rectTransform.sizeDelta.x * normalPos.x, minimapImage.rectTransform.sizeDelta.y * normalPos.y);
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        sabotage.SetActive(false);
        if (TaskManager.instance.taskIndexs.Count > 0)
        {
            for (int i = 0; i < TaskManager.instance.taskIndexs.Count; i++)
            {
                Vector2 taskPos = new Vector2(Vector3.Distance(left.position, new Vector3(TaskManager.instance.responsiveObject[TaskManager.instance.taskIndexs[i]].transform.position.x, 0f, 0f)),
                        Vector3.Distance(bottom.position, new Vector3(0f, TaskManager.instance.responsiveObject[TaskManager.instance.taskIndexs[i]].transform.position.y, 0f)));
                Vector2 normalPos = new Vector2(taskPos.x / MapArea.x, taskPos.y / MapArea.y);
                Image minimapMissionIamge = taskExclamationMarkScript.PopObject().GetComponent<Image>();
                minimapMissionIamge.rectTransform.anchoredPosition = new Vector2(minimapImage.rectTransform.sizeDelta.x * normalPos.x, minimapImage.rectTransform.sizeDelta.y * normalPos.y);
            }
        }
    }

    public void OpenSabotage()
    {
        gameObject.SetActive(true);
        taskExclamationMarkScript.PushObject();
        sabotage.SetActive(true);
    }

    public void Close()
    {
        taskExclamationMarkScript.PushObject();
        sabotage.SetActive(false);
        gameObject.SetActive(false);
    }



}
