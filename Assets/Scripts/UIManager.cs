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

    public GameObject logicGatePanel;
    public List<TextMeshProUGUI> buttonStateText;
    public List<bool> buttonState;
    public GameObject logicOutput;
    bool firstGate;
    bool secondGate;
    bool thirdGate ;
    bool fourthGate;
    bool secondLayerFirstGate;
    bool secondLayerSecondGate;
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
    public void LogicGatesStartFinish(bool state)
    {
         logicGatePanel.SetActive(state);
         firstGate = false;
         secondGate = false;
         thirdGate = false;
         fourthGate = false;
         secondLayerFirstGate = false;
         secondLayerSecondGate = false;
    }
    public void ChangeButtonBool(int index)
    {
        buttonState[index] = !buttonState[index];
        buttonStateText[index].text = buttonState[index].ToString();
        LogicGatesGame();
    }
    public void LogicGatesGame()
    {
        if(buttonState[0] || buttonState[1])
        {
            firstGate = true;
            Debug.Log("First Gate out " + firstGate);
        }
        if(buttonState[2] && buttonState[3])
        {
            secondGate = true;
            Debug.Log("Second Gate out " + secondGate);
        }
        if(buttonState[4] || buttonState[5])
        {
            thirdGate = true;
            Debug.Log("third Gate out " + thirdGate);
        }
        if(buttonState[6] && buttonState[7])
        {
            fourthGate = true;
    
        }
        if(firstGate || secondGate)
        {
            secondLayerFirstGate = true;
            Debug.Log("secondFirst Gate out " + secondLayerFirstGate);
        }
        if(thirdGate && fourthGate)
        {
            secondLayerSecondGate = true;
            Debug.Log("secondSecond Gate out " + secondLayerSecondGate);
        }

        if((secondLayerFirstGate && !secondLayerSecondGate) || (!secondLayerFirstGate && secondLayerSecondGate))
        {
            logicOutput.GetComponent<Image>().color = Color.green;
            StartCoroutine(FinishLogicGates());
        }

    }
    public IEnumerator FinishLogicGates()
    {
        yield return new WaitForSeconds(2f);
        LogicGatesStartFinish(false);
    }
    public void FormatGameNext()
    {
        if(i == 0)
        {
            formatText.text = "Siz Sadece Next'e Basın ve Arkanıza Yaslanın.";
            i += 1;
            Debug.Log("hi");
        }
        else if (i == 1)
        {
            formatText.text = "Birazcık Daha Next.";
            i += 1;
        }
        else if(i == 2)
        {
            formatText.text = "Next... Next... Next...";
        }
        else if (i == 3)
        {
            formatText.text = "Tebrikler Başarıyla Format Attınız! Zor Olmasa Gerek :)";
            formatNextText.text = "FINISH";
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
