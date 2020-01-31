using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.holdingObject = Helper.SendRay();
            
            DrawOutLine.SetHighLighted(GameManager.holdingObject);
        }
        if (Input.GetMouseButton(0))
        {
            if (GameManager.holdingObject.CompareTag("Part"))
            {
                float distance = GameManager.holdingObject.transform.position.z - Camera.main.transform.position.z;
                Vector3 pos = Camera.main.ScreenToWorldPoint((new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance)));
                GameManager.holdingObject.GetComponent<Rigidbody>().MovePosition(pos);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            DrawOutLine.SetStandard(GameManager.holdingObject);

            GameManager.holdingObject = null;
        }
    }
}
