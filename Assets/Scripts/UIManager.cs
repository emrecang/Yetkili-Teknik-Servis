using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI partInfoText;
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void ChangePartText(string text)
    {
        partInfoText.text = text;
    }
}
