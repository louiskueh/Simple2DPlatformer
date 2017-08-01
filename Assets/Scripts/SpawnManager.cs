using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public int maxPlatforms = 20;
    public GameObject platform;

    // Spawning platforms of distance of 6.5 - 14 units
    public float horizontalMin = 12f;
    public float horizontalMax = 15f;

    // Platforms up/below 6.5 - 14 units
    public float verticalMin = -6f;
    public float verticalMax = 4f;

    private Vector2 originPosition;

    // Use this for initialization
    void Start()
    {
        originPosition = transform.position;
        Spawn();
    }
    void Spawn()
    {
        for (int i = 0; i < maxPlatforms; i++)
        {
            Vector2 randomPosition = originPosition + new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(verticalMin,verticalMax));
            // Quentarion for no rotation
            Instantiate(platform, randomPosition, Quaternion.identity);
            originPosition = randomPosition;
        }
    }


}