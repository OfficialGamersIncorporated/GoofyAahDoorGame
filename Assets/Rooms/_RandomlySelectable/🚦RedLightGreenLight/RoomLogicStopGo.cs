using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogicStopGo : RoomLogic
{
    public float OpenDelay = 2f;

    bool red;
    bool yellow;
    bool green;

    bool spawningBombs = false;

    [SerializeField] GameObject bomb;
    [SerializeField] GameObject bombSpawnPoint;
    public int numberOfBombs = 50;
    [SerializeField] GameObject trafficLight;
    SpriteRenderer lightSpriteRenderer;
    [SerializeField] Sprite redLight;
    [SerializeField] Sprite yellowLight;
    [SerializeField] Sprite greenLight;

    public float lightTimer;
    public float MaxTimerValue = 3f;
    public float MinTimerValue = 0.5f;

    new void Start()
    {
        base.Start();
        OpenExitWithDelay(OpenDelay);

        lightSpriteRenderer = trafficLight.GetComponent<SpriteRenderer>();

        ChangeGreen();
        NewTimer();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (red)
            {
                if (!spawningBombs)
                {
                    SpawnBombs(numberOfBombs);
                }
                
            }
        }

        lightTimer -= Time.deltaTime;
        
        if (lightTimer < 0 )
        {
            ChangeLight();
        }
    }

    void ChangeLight()
    {
        if (red)
        {
            NewTimer();
            ChangeGreen();
        }
        else if (yellow)
        {
            NewTimer();
            ChangeRed();
        }
        else if (green)
        {
            lightTimer = 0.5f;
            ChangeYellow();
        }
    }

    void SpawnBombs(int bombsToSpawn)
    {
        spawningBombs = true;
        for (int i = 0; i < bombsToSpawn; i++)
        {
            GameObject spawnedBomb = Instantiate(bomb, bombSpawnPoint.transform.position, Quaternion.identity);
            spawnedBomb.GetComponent<Rigidbody2D>().AddForce(UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(0.5f, 10f), ForceMode2D.Force);
        }
    }

    void ChangeRed()
    {
        red = true;
        yellow = false;
        green = false;
        lightSpriteRenderer.sprite = redLight;
    }

    void ChangeYellow()
    {
        red = false;
        yellow = true;
        green = false;
        lightSpriteRenderer.sprite = yellowLight;
    }

    void ChangeGreen()
    {
        red = false;
        yellow = false;
        green = true;
        lightSpriteRenderer.sprite = greenLight;
    }

    void NewTimer()
    {
        lightTimer = UnityEngine.Random.Range(MinTimerValue, MaxTimerValue);
    }

}
