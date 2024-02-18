using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DanceRoomLogic : RoomLogic
{
    public Slider slider;
    public Image sliderFill;

    public AudioSource music;

    bool startDance = false;
    bool openedDoor = false;

    float danceAmount;
    public float danceObjective = 100f;
    public float danceRate = 20f;
    public float decreaseRate = 5f;

    new void Start()
    {
        base.Start();
        StartCoroutine(ObjectiveDelayStart());

        slider.minValue = 0;
        slider.maxValue = danceObjective;
        slider.value = 5f;
        danceAmount = 5f;
    }

    void Update()
    {
        if (startDance)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                danceAmount += Time.deltaTime * danceRate;
                sliderFill.color = Color.green;
            }
            else
            {
                if (danceAmount > 0 && danceAmount < danceObjective)
                {
                    danceAmount -= Time.deltaTime * decreaseRate;
                    sliderFill.color = Color.red;
                }
            }
            slider.value = danceAmount;
        }
        if (danceAmount > danceObjective)
        {
            if (!openedDoor)
            {
                OpenExitDoor();
                openedDoor = true;
            }
            
        }
    }


    IEnumerator ObjectiveDelayStart()
    {
        yield return new WaitForSeconds(2f);
        startDance = true;
        music.Play();
    }

}
