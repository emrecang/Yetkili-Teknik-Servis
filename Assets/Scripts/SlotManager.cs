using System.Collections;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public SlotType slot;

    public Coroutine snapCor;
    public bool snapped;


    public void Detach()
    {
        snapped = false;
        GetComponent<BoxCollider>().enabled = true;
        GameManager.holdingObject.GetComponent<PartData>().attachedSlot = null;
    }

    public IEnumerator Snap()
    {
        GameObject snapObj = GameManager.holdingObject;
        while (Vector3.Distance(snapObj.transform.position, transform.position) > 0.001f)
        {
            snapObj.transform.position = Vector3.Lerp(snapObj.transform.position, transform.position, 3f * Time.deltaTime);
            snapObj.transform.rotation = Quaternion.Lerp(snapObj.transform.rotation, transform.rotation, 3f * Time.deltaTime);
            yield return new WaitForSeconds(0.001f);
        }
        snapped = true;
        GetComponent<BoxCollider>().enabled = false;
        snapObj.GetComponent<PartData>().attachedSlot = this;
        Debug.Log("Hi");
    }
}
