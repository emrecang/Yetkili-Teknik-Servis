using System.Collections;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public SlotType slot;

    public Coroutine snapCor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PartData>().type == slot)
        {
            snapCor = StartCoroutine("Snap", other.transform.gameObject);
        }
    }
    
    
    
    IEnumerator Snap(GameObject snapObj)
    {
        while(Vector3.Distance(snapObj.transform.position, transform.position) < 0f)
        {
            snapObj.transform.position = Vector3.Lerp(snapObj.transform.position, transform.position, 0.1f);
            snapObj.transform.rotation = Quaternion.Lerp(snapObj.transform.rotation, transform.rotation, 0.1f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
