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


    private void OnEnable()
    {
        card.anchoredPosition = new Vector2(cardStartPoint.x, cardStartPoint.y);
        card.localScale = new Vector3(.8f, .8f, 1);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
