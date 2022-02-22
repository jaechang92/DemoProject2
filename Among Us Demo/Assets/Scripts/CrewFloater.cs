using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewFloater : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private List<Sprite> sprites;

    private bool[] crewStates = new bool[12];
    private float timer = 0.5f;
    private float distance = 11.0f;

    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {
        if (!crewStates[(int)playerColor])
        {
            crewStates[(int)playerColor] = true;

            float angle = Random.Range(0.0f, 360.0f);
            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * dist;
            Vector3 direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);

            float floatingSpeed = Random.Range(1.0f, 4.0f);
            float rotateSpeed = Random.Range(-3.0f, 3.0f);


            FloatingCrew crew = Instantiate(prefab, spawnPos, Quaternion.identity).GetComponent<FloatingCrew>();
            crew.SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], playerColor, direction, floatingSpeed, rotateSpeed, Random.Range(0.5f, 1.0f));


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FloatingCrew crew = collision.GetComponent<FloatingCrew>();
        if (crew != null)
        {
            crewStates[(int)crew.playerColor] = false;
            Destroy(crew.gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            SpawnFloatingCrew((EPlayerColor)i, Random.Range(0, distance));
            timer = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance);
            timer = 1.0f;
        }

    }
}
