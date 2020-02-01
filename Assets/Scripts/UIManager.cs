using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI partInfoText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI hourText;
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void ChangePartText(string text)
    {
        partInfoText.text = text;
    }
    public void ChangeDayText(string text)
    {
        dayText.text = text;
    }
    public void ChangeHourText(string text)
    {
        hourText.text = text;
    }
}
