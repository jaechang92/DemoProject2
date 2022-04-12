using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCardMission : MonoBehaviour
{
    [SerializeField]
    private Vector2 cardStartPoint;
    [SerializeField]
    private Vector2 cardEndPoint;
    [SerializeField]
    private RectTransform card;

    [SerializeField]
    private float min, max;

    private float currentTime = 0;
    private bool isChecked = false;
    private bool isClear = false;
    private void OnEnable()
    {
        card.anchoredPosition = new Vector2(cardStartPoint.x, cardStartPoint.y);
        card.localScale = new Vector3(.8f, .8f, 1);

    }

    private void Update()
    {
        card.anchoredPosition = Vector2.MoveTowards(card.anchoredPosition, cardEndPoint, 300 * Time.deltaTime);
        card.localScale = Vector3.MoveTowards(card.localScale, Vector3.one, Time.deltaTime);
        if (isChecked)
        {
            currentTime += Time.deltaTime;
        }


        if (Input.GetMouseButton(0))
        {
            card.anchoredPosition= new Vector2(Input.mousePosition.x, card.anchoredPosition.y);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == card.gameObject)
        {
            isChecked = true;
            currentTime = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == card.gameObject)
        {
            isChecked = false;
            if (currentTime >= min && currentTime <= max)
            {
                isClear = true;
                Invoke("CloseUI", 1.0f);
            }
        }
    }

    private void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
