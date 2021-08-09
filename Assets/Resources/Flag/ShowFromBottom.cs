using UnityEngine;

public class ShowFromBottom : MonoBehaviour
{
    private float showHeight=0;
    private Material mat;
    private float maxHeight = 4;

    private void Awake()
    {
        mat= GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (showHeight >= maxHeight)
        {
            return;
        }
        showHeight += Time.deltaTime * 10;
        mat.SetFloat("_height",showHeight);
    }

    private void OnDisable()
    {
        showHeight = 0;
    }
}
