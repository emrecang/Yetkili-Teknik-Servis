using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0f,1f)]
    public float scrollMultiplier;
    public float maxDistance;
    public float minDistance;
    public float rotationSpeed;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            GameManager.holdingObject = Helper.SendRay();

            if (GameManager.holdingObject == null)
            {
                return;
            }

            if (GameManager.holdingObject.CompareTag("Device"))
            {
                GameManager.instance.brokenDevice = GameManager.holdingObject.GetComponent<DeviceManager>();
                GameManager.holdingObject = null;
                GameManager.instance.RepairGameState();
            }


            if (GameManager.holdingObject.CompareTag("Part") && 
                GameManager.holdingObject.GetComponent<PartData>().attachedSlot != null)
            {
                GameManager.holdingObject.GetComponent<PartData>().attachedSlot.Detach();
            }

            

            DrawOutLine.SetHighLighted(GameManager.holdingObject);
        }
        
        if (Input.GetMouseButton(0))
        {

            if (GameManager.holdingObject == null)
            {
                return;
            }

            Debug.DrawRay(GameManager.holdingObject.transform.position, GameManager.holdingObject.transform.forward * 10, Color.red);

            if (GameManager.holdingObject.CompareTag("Part"))
            {
                float distance = Mathf.Min(
                                    Mathf.Max(Mathf.Abs(Camera.main.transform.position.z + minDistance), 
                                    Mathf.Abs(GameManager.holdingObject.transform.position.z - Camera.main.transform.position.z 
                                    - (Input.mouseScrollDelta.y * scrollMultiplier)))
                                    , Camera.main.transform.position.z + maxDistance);
                Vector3 pos = Camera.main.ScreenToWorldPoint((new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance)));
                GameManager.holdingObject.GetComponent<Rigidbody>().MovePosition(pos);
                GameManager.holdingObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if (GameManager.holdingObject == null)
            {
                return;
            }

            if (GameManager.holdingObject.CompareTag("Part"))
            {
                GameObject candidateObject = Helper.SendRay(GameManager.holdingObject.transform.position, GameManager.holdingObject.transform.forward);
                if (candidateObject != null && candidateObject.CompareTag("box"))
                {
                    UIManager.instance.ChangeToolBox(true);
                }
                Debug.Log("PlayerController girdi");

                if (candidateObject!= null && candidateObject.TryGetComponent<SlotManager>(out SlotManager candidateSlot))
                {
                    Debug.Log(candidateSlot.name);
                    if(candidateSlot.snapCor == null)
                    {
                        candidateSlot.snapCor = StartCoroutine(candidateSlot.Snap());
                    }
                    else
                    {
                        StopCoroutine(candidateSlot.Snap());
                        candidateSlot.snapCor = null;
                        candidateSlot.snapCor = StartCoroutine(candidateSlot.Snap());
                    }
                }

                DrawOutLine.SetStandard(GameManager.holdingObject);

                GameManager.holdingObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GameManager.holdingObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                GameManager.holdingObject = null; 
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            GameManager.holdingObject = Helper.SendRay();

            if (GameManager.holdingObject == null)
            {
                return;
            }

            DrawOutLine.SetHighLighted(GameManager.holdingObject);
        }
        if (Input.GetMouseButton(1))
        {
            if (GameManager.holdingObject == null)
            {
                return;
            }

            if (GameManager.holdingObject.CompareTag("Part"))
            {

                GameManager.holdingObject.transform.Rotate(
                    new Vector3(Input.GetAxis("Mouse X")  * rotationSpeed, Input.GetAxis("Mouse Y")  * rotationSpeed, 
                    Input.mouseScrollDelta.y * rotationSpeed / 2));
                GameManager.holdingObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (GameManager.holdingObject == null)
            {
                return;
            }
            if (GameManager.holdingObject.CompareTag("Part"))
            {
                GameObject candidateObject = Helper.SendRay(GameManager.holdingObject.transform.position,
                    GameManager.holdingObject.transform.forward);
                if (candidateObject != null && candidateObject.TryGetComponent<SlotManager>(out SlotManager candidateSlot) == true)
                {
                    if (candidateSlot.snapCor == null)
                        StartCoroutine(candidateSlot.Snap());
                }
                DrawOutLine.SetStandard(GameManager.holdingObject);

                GameManager.holdingObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GameManager.holdingObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                GameManager.holdingObject = null;
            }
        }
    }
}
