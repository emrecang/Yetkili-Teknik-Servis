using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SlotType 
{
    RAM,
    CPU,
    GPU,
    FAN,
    PSU
}
public class Helper : MonoBehaviour
{
    
    public static GameObject SendRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.collider.gameObject;
        }
        else
        {
            return new GameObject();
        }
    }
}
