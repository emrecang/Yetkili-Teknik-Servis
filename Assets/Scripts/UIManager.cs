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
    public GameObject formatPanel;
    public TextMeshProUGUI formatText;
    public TextMeshProUGUI formatNextText;
    private void Awake()
    {
        instance = this;
    }
    int i = 0;
    public void FormatGame(bool state)
    {
        formatPanel.SetActive(state);
        formatText.text = "Format Atmaya Hoş Geldiniz.";
        formatNextText.text = "NEXT";
    }
    public void FormatGameNext()
    {
        if(i == 0)
        {
            formatText.text = "Format Atmaya Hoş Geldiniz.";
            i += 1;
        }
        else if(i == 1)
        {
            formatText.text = "Siz Sadece Next Diyin ve Arkanıza Yaslanın";
            i += 1;
            Debug.Log("hi");
        }
        else if (i == 2)
        {
            formatText.text = "Birazcık Daha Next";
            i += 1;
        }
        else if (i == 3)
        {
            formatText.text = "Tebrikler Format Başarıyla Atıldı!";
            formatNextText.text = "Finish";
            i += 1;
        }
        else
        {
            FormatGame(false);
        }
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
