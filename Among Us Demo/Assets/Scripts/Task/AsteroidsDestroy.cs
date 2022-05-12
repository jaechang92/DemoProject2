using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AsteroidsDestroy : ClearChecker , IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Asteroid"))
        {
            AsteroidController target = eventData.pointerCurrentRaycast.gameObject.GetComponent<AsteroidController>();
            target.GetComponent<Image>().sprite = asteroidDestroySprite[target.idx];
            target.Destroyed();
            destroyCount--;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    [SerializeField]
    private GameObject spawnOrigin;
    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private float spawnSpeed;
    [SerializeField]
    private List<Sprite> asteroidSprite;
    [SerializeField]
    private List<Sprite> asteroidDestroySprite;

    [SerializeField]
    private Transform spawnParent;

    [SerializeField]
    private int destroyCount;

    float spawnTime;
    float currentTime = 0;
    void Start()
    {
        spawnTime = Random.Range(0.2f, 0.8f);
        destroyCount = 10;
    }


    void Update()
    {
        if (isClear) return;
        currentTime += Time.deltaTime;
        if (currentTime >= spawnTime)
        {
            currentTime = 0;
            spawnTime = Random.Range(0.2f, 0.8f);
            AsteroidSpawn();
        }

        if (destroyCount <= 0)
        {
            isClear = true;
            InGameUIManager.Instance.CloseTaskUI(gameObject, 1.0f);
        }
        
    }

    void AsteroidSpawn()
    {
        float spawnPotionRandom = Random.Range(0.0f, 1.0f);
        int RandomSpriteIndex = Random.Range(0, asteroidSprite.Count);
        Vector3 spawnPosition = new Vector3(spawnPoints[0].position.x, Mathf.Lerp(spawnPoints[0].position.y, spawnPoints[1].position.y, spawnPotionRandom),0);
        GameObject inst = Instantiate(spawnOrigin, spawnPosition, Quaternion.identity, spawnParent);
        inst.GetComponent<Image>().sprite = asteroidSprite[RandomSpriteIndex];
        inst.GetComponent<AsteroidController>().idx = RandomSpriteIndex;
    }


}
