using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    public List<SlotManager> slots;

    public Transform cameraTransform;

    public void Start()
    {
        cameraTransform = transform.GetChild(0).transform;
    }

    //UI butonu üzerinde (finish)
    public void CheckPC()
    {
        string brokenPartsText = "";
        bool isPCOK = true;
        List<PartData> brokenParts = new List<PartData>();
        int brokenRamCount = 0;
        foreach(var slot in slots)
        {
            if (slot.attachedPart.isBroken)
            {
                if(slot.attachedPart.type == SlotType.RAM)
                {
                    brokenRamCount++;
                    brokenParts.Add(slot.attachedPart);
                }
                else
                {
                    brokenParts.Add(slot.attachedPart);
                    isPCOK = false;
                }
                brokenPartsText += slot.attachedPart.type.ToString() + "\n ";
            }
            if(brokenRamCount == 2)
            {
                isPCOK = false;
            }
        }

        if (!isPCOK)
        {
            //PC hala bozuk
            brokenPartsText += "Hala Bozuk.";
            StartCoroutine(UIManager.instance.brokenInfo(brokenPartsText));
        }
        else
        {
            //PC tamir edildi.
            brokenPartsText = "Her Şey Yolunda.";
            StartCoroutine(UIManager.instance.brokenInfo(brokenPartsText));
            StartCoroutine(WaitLittle());
            GameManager.instance.brokenDevice.transform.position = GameManager.instance.cust.transform.position;
            UIManager.instance.CheckButtonState(false);
            GameManager.instance.ChangeMoneyValue(100);
            GameManager.instance.dailyProfit += 100;

        }
    }
    public IEnumerator WaitLittle()
    {
        yield return new WaitForSeconds(4f);
        GameManager.instance.WaitCustomerGameState();
    }
}
