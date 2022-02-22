using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class TaskListUI : MonoBehaviour, IPointerClickHandler 
{
    [SerializeField]
    private float offset;
    [SerializeField]
    private RectTransform taskListUITransform;

    private bool isOpen = false;
    private float timer;

    public void OnPointerClick(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(OpenAndHideUI());
    }


    private IEnumerator OpenAndHideUI()
    {
        isOpen = !isOpen;

        if (timer != 0f)
        {
            timer = 1f - timer;
        }

        while (timer <= 1f)
        {
            timer += Time.deltaTime * 2f;
            float start = isOpen ? -taskListUITransform.sizeDelta.x : offset;
            float dest = isOpen ? offset : -taskListUITransform.sizeDelta.x;
            taskListUITransform.anchoredPosition = new Vector2(Mathf.Lerp(start, dest, timer), taskListUITransform.anchoredPosition.y);
            yield return null;

        }

    }



}
