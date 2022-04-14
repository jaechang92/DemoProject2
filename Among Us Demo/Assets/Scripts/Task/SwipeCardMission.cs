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

    [SerializeField]
    private float currentTime = 0;
    private bool isChecked = false;
    [SerializeField]
    private bool isClear = false;
    private void OnEnable()
    {
        card.anchoredPosition = new Vector2(cardStartPoint.x, cardStartPoint.y);
        card.localScale = new Vector3(.8f, .8f, 1);

    }

    private void Update()
    {
        SetCardPosition();
        if (Input.GetMouseButton(0) && isChecked)
        {
            currentTime += Time.deltaTime;
            
            card.transform.position = new Vector3(Input.mousePosition.x, card.transform.position.y, card.transform.position.z);

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
        if (collision.gameObject == card.gameObject && Input.GetMouseButton(0))
        {
            isChecked = false;
            if (currentTime >= min && currentTime <= max)
            {
                isClear = true;
                Invoke("CloseUI", 3.0f);
            }
        }
    }

    private void SetCardPosition()
    {
        if (!isClear)
        {
            card.anchoredPosition = Vector2.MoveTowards(card.anchoredPosition, cardEndPoint, 500 * Time.deltaTime);
            card.localScale = Vector3.MoveTowards(card.localScale, Vector3.one, Time.deltaTime);
        }
        else
        {
            card.anchoredPosition = Vector2.MoveTowards(card.anchoredPosition, cardStartPoint, 500 * Time.deltaTime);
            card.localScale = Vector3.MoveTowards(card.localScale, new Vector3(.8f, .8f, 1), Time.deltaTime);
        }
        
    }
    private void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
