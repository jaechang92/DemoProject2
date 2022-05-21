using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVUI : MonoBehaviour
{
    public List<Animator> CCTVAniList;

    private void OnEnable()
    {
        for (int i = 0; i < CCTVAniList.Count; i++)
        {
            CCTVAniList[i].SetBool("Used", true);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < CCTVAniList.Count; i++)
        {
            CCTVAniList[i].SetBool("Used", false);
        }
    }

    public void SimpleClose()
    {
        gameObject.SetActive(false);
    }

}
