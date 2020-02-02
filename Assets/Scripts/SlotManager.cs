using System.Collections;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public SlotType slot;

    public Coroutine snapCor;

    public PartData attachedPart;

    public float offset;
    public void Detach()
    {
        GetComponent<BoxCollider>().enabled = true;
        GameManager.holdingObject.GetComponent<PartData>().attachedSlot = null;
        attachedPart = null;
    }

    public IEnumerator Snap()
    {
        GameObject snapObj = GameManager.holdingObject;
        while (Vector3.Distance(snapObj.transform.position, transform.position) > offset)
        {
            snapObj.transform.position = Vector3.Lerp(snapObj.transform.position, transform.position, 0.5f * Time.deltaTime);
            snapObj.transform.rotation = Quaternion.Lerp(snapObj.transform.rotation, transform.rotation, 1f * Time.deltaTime);
            yield return new WaitForSeconds(0.001f);
        }
        GetComponent<BoxCollider>().enabled = false;
        snapObj.GetComponent<PartData>().attachedSlot = this;
        attachedPart = snapObj.GetComponent<PartData>();
        Debug.Log("Hi");
    }
}
