using UnityEngine;

public class DrawOutLine : MonoBehaviour
{
    public static Shader myShader;
    public static Shader standard;
    private void Start()
    {
        standard = Shader.Find("Standard");
        myShader = Shader.Find("Self-Illumin/Outlined Diffuse");
    }
    public static void SetStandard(GameObject selected)
    {
        if (selected.CompareTag("Part"))
        {
            selected.GetComponent<MeshRenderer>().material.shader = standard;
            UIManager.instance.ChangePartText("");
        }
    }

    public static void SetHighLighted(GameObject selected)
    {
        if (selected.CompareTag("Part"))
        {
            selected.GetComponent<MeshRenderer>().material.shader = myShader;
            UIManager.instance.ChangePartText(selected.GetComponent<PartData>().partName);
        }
    }
}
