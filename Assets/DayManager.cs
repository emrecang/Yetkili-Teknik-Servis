using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int day;
    public int clockHour;
    public int clockMinute;

    void Start()
    {
        day = 1;
    }

    public void Calculate()
    {
        if(60 - clockMinute <= 0)
        {
            clockMinute = 0;
            clockHour += 1;
        }
        if(clockHour >= 24)
        {
            clockHour = 0;
            day += 1;
            //Call day ui;
        }
    }
    public void IncreaseMin(int value)
    {
        clockMinute += value;
        Calculate();
        UIManager.instance.ChangeHourText(String.Format("{0:00}:{1:00}", clockHour, clockMinute));
        //UIManager.instance.ChangeHourText(clockHour.ToString() + ":" + clockMinute.ToString());
        UIManager.instance.ChangeDayText(day.ToString() + ". Day");
    }
    public void Update()
    {
        IncreaseMin(1);
    }
}
