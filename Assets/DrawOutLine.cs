using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawOutLine : MonoBehaviour
{
    public Shader myShader;
    GameObject selected;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = SendRay();
        }
        if (Input.GetMouseButton(0))
        {
            if (selected.CompareTag("Part"))
            {
                selected.GetComponent<MeshRenderer>().material.shader = myShader;
                    UIManager.instance.ChangePartText(selected.GetComponent<PartData>().partName);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(selected != null)
            {
                if (selected.CompareTag("Part"))
                {
                    selected.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
                    UIManager.instance.ChangePartText("");
                }
                selected = null;
            }
        }
    }
    private GameObject SendRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform.gameObject;
        }
        else
        {
            return null;
        }
    }

}
