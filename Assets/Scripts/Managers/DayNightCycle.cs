using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class DayNightCycle : SingletonMonoBehaviour<DayNightCycle>
{
    [SerializeField] private Gradient gradient;
    [SerializeField] private GameObject lightSource;

    private int days;
    public int GetDays()
    {
        return days;
    }
   [SerializeField] private float time = 50f;
    private float fullDay = 500f;
    private bool canChangeDay = true;

    public float timeOfSunset;
    public float timeOfSunrise;




    public bool IsDay()
    {
        if (time < timeOfSunset || time > timeOfSunrise)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void Update()
    {
        if (time > fullDay)
        {
            time = 0;
        }

        if (time > 250 && time < 255 && canChangeDay)
        {

            days++;
            canChangeDay = false;
        }
        if (time > 260)
        {
            canChangeDay = true;
        }

        time += Time.deltaTime;
        lightSource.GetComponent<Light2D>().color = gradient.Evaluate(time / fullDay);
    }
}
