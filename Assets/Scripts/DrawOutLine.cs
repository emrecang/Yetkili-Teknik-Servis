using UnityEngine;

public class DrawOutLine : MonoBehaviour
{
    public Shader myShader;
    public Shader standart;
    private void Start()
    {
        standart = Shader.Find("Standart");
    }
    public void SetStandart(GameObject selected)
    {
        if (selected.CompareTag("Part"))
        {
            selected.GetComponent<MeshRenderer>().material.shader = standart;
            UIManager.instance.ChangePartText("");
        }
    }

    public void SetHighLighted(GameObject selected)
    {
        if (selected.CompareTag("Part"))
        {
            selected.GetComponent<MeshRenderer>().material.shader = myShader;
            UIManager.instance.ChangePartText(selected.GetComponent<PartData>().partName);
        }
    }
}
