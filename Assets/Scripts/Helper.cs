using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SlotType 
{
    RAM,
    CPU,
    GPU,
    HDD,
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
            return null;
        }
    }

    public static GameObject SendRay(Vector3 from, Vector3 to)
    {
        
        if (Physics.Raycast(from, to, out RaycastHit hit, 10f))
        {
            return hit.collider.gameObject;
        }
        else
        {
            Debug.Log("boş");
            return null;
        }
    }

    IEnumerator Snap(GameObject snapObj)
    {
        Debug.Log("Hi");
        while (Vector3.Distance(snapObj.transform.position, transform.position) > 0f)
        {
            snapObj.transform.position = Vector3.Lerp(snapObj.transform.position, transform.position, 1f * Time.deltaTime);
            snapObj.transform.rotation = Quaternion.Lerp(snapObj.transform.rotation, transform.rotation, 1f * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
