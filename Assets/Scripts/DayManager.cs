using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int day;
    public int clockHour;
    public int clockMinute;
    public float dailyMin;
    public static DayManager instance;
    Coroutine dayCor;
    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        dailyMin = 0;
        day = 1;
    }

    public void CalculateDate()
    {
        if(clockHour < 9)
        {
            clockHour = 9;
        }
        if(60 - clockMinute <= 0)
        {
            clockMinute = 0;
            clockHour += 1;
        }
        
        
    }
    public IEnumerator IncreaseMin(int minValue,int timeValue)
    {
        clockMinute += minValue;
        dailyMin += minValue;
        float value = (720f - dailyMin) / 720f;
        UIManager.instance.ChangeClockFillAmount(value);
        CalculateDate();
        
        UIManager.instance.ChangeHourText(String.Format("{0:00}:{1:00}", clockHour, clockMinute));
        UIManager.instance.ChangeDayText(day.ToString() + ". Day");
        if (clockHour >= 18)
        {
            clockHour = 8;
            dailyMin = 0;
            day += 1;
            UIManager.instance.ChangeDayImage(true);
            UIManager.instance.ChangeDayImageText(day.ToString() + ". GÜN");
            yield return new WaitForSeconds(2f);
            UIManager.instance.ChangeDayImage(false);
            UIManager.instance.ChangeClockFillAmount(1);
        }
        yield return new WaitForSeconds(timeValue);
        StartCoroutine(IncreaseMin(minValue, timeValue));
    }

    public void StartIncreaseMinCor(int minValue, int timeValue)
    {
        dayCor = StartCoroutine(IncreaseMin(minValue, timeValue));
    }
    public void StopIncreaseMinCor()
    {
        dayCor = null;
    }
}
