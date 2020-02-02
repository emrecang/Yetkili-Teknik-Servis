using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI partInfoText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI hourText;
    public TextMeshProUGUI dayImageText;
    public TextMeshProUGUI brokenText;

    public GameObject analogClock;
    public GameObject dayImage;
    public GameObject toolbox;
    public GameObject shopPanel;
    private void Awake()
    {
        instance = this;
    }

    public IEnumerator brokenInfo(string text)
    {
        brokenText.text = text;

        yield return new WaitForSeconds(2f);

        brokenText.text = "";
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
    public void ChangeToolBox(bool state)
    {
        toolbox.SetActive(state);
    }
    public void ChangeDayImage(bool state)
    {
        dayImage.SetActive(state);
    }
    public void ChangeDayImageText(string text)
    {
        dayImageText.text = text;
    }
    public void ChangeClockFillAmount(float value)
    {
        analogClock.GetComponent<Image>().fillAmount = value;
    }
}
