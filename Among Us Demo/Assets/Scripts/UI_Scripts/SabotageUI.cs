using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum E_Sabotage
{
    sabotage_reactor,
    sabotage_O2,
    sabotage_electricity,
    sabotage_comms,
    sabotage_Doors,
}
public class SabotageUI : MonoBehaviour
{
    public E_Sabotage sabotage;
    [SerializeField]
    public List<Transform> sabotageObjects;
    [SerializeField]
    private float timer = 1;
    [SerializeField]
    private Image fillAmountImage;
    [SerializeField]
    private Button btn;



    public void SetTimerImage()
    {
        fillAmountImage.fillAmount = 1;
        StartCoroutine(Co_Timer());
    }
    public void SetInteractable(bool state)
    {
        btn.interactable = state;
    }
    IEnumerator Co_Timer()
    {
        while (fillAmountImage.fillAmount > 0)
        {
            fillAmountImage.fillAmount -= Time.deltaTime / timer;
            yield return null;
        }
    }
}
