using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int day;
    public int clockHour;
    public int clockMinute;
    public static DayManager instance;
    Coroutine dayCor;
    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        day = 1;
    }

    public void CalculateDate()
    {
        if(clockHour < 8)
        {
            clockHour = 8;
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
        CalculateDate();
        
        UIManager.instance.ChangeHourText(String.Format("{0:00}:{1:00}", clockHour, clockMinute));
        UIManager.instance.ChangeDayText(day.ToString() + ". Day");
        if (clockHour >= 12)
        {
            clockHour = 8;
            day += 1;
            UIManager.instance.ChangeDayImage(true);
            UIManager.instance.ChangeDayImageText(day.ToString() + ". GÜN");
            yield return new WaitForSeconds(2f);
            UIManager.instance.ChangeDayImage(false);
        }
        yield return new WaitForSeconds(timeValue);
        StartCoroutine(IncreaseMin(minValue, timeValue));
    }

    public void StartIncreaseMinCor(int minValue, int timeValue)
    {
        StartCoroutine(IncreaseMin(minValue, timeValue));
    }
    public void StopIncreaseMinCor()
    {
        dayCor = null;
    }
}
