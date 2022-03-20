using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AsteroidsDestroy : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private float spawnSpeed;
    [SerializeField]
    private List<Sprite> asteroidSprite;

    [SerializeField]
    private Transform spawnParent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
