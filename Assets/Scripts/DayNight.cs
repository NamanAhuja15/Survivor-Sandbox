using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNight : MonoBehaviour
{
    public Light sun;
    public float secondsInFullDay = 120f;
    private float days;
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    public float timeMultiplier = 1f;
    public Text hour, minutes,day;
   // public Transform hourHand;
   // public Transform minuteHand;    
    float hoursToDegrees = 360f / 12f;
    float minutesToDegrees = 360f / 60f;

    float sunInitialIntensity;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
        days = 1;
    }

    void Update()
    {
        UpdateSun();
        UpdateClock();
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
            days++;
        }
    }
    void UpdateClock()
    {
        float currentHour = 24 * currentTimeOfDay;
        float currentMinute = 60 * (currentHour - Mathf.Floor(currentHour));
        hour.text = currentHour.ToString("0")+":";
        minutes.text = currentMinute.ToString("0");
        day.text = days.ToString("0");
      //  hourHand.localRotation = Quaternion.Euler(currentHour * hoursToDegrees, 0, 0);
      //  minuteHand.localRotation = Quaternion.Euler(currentMinute * minutesToDegrees, 0, 0);
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}

