using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShieldMission : MonoBehaviour
{
    [SerializeField]
    private List<Image> shield_Panels;
    [SerializeField]
    private Color redColor;
    [SerializeField]
    private int missionCnt;
    private List<int> missionIdx = new List<int>();
    [SerializeField]
    private List<Animator> lamps;

    private bool isClear = false;    
    void Start()
    {
        while (missionCnt > 0)
        {
            int idx  = Random.Range(0, shield_Panels.Count);
            if(!missionIdx.Contains(idx))
            {
                missionIdx.Add(idx);
                shield_Panels[idx].color = redColor;
                missionCnt--;
            }

        }
    }

    public void ClickShield_Panel()
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.white;

        for (int i = 0; i < shield_Panels.Count; i++)
        {
            if (shield_Panels[i].color != Color.white)
            {
                return;
            }
        }
        isClear = true;
        foreach (var item in lamps)
        {
            item.SetBool("On", true);
        }
        InGameUIManager.Instance.CloseTaskUI(gameObject, 1.0f);
    }

}
