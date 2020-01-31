using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawOutLine : MonoBehaviour
{
    public Shader myShader;
    public TextMeshPro text;
    void OnMouseOver()
    {
        transform.GetComponent<MeshRenderer>().material.shader = myShader;
        text.transform.position = new Vector3(transform.position.x + 0.4f,transform.position.y + 0.7f,transform.position.z);
        text.text = "RAM";
    }
    void OnMouseExit()
    {
        transform.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
        text.text = "";
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
