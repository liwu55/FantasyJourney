using System;
using UnityEngine;

public class FaceCameraOnlyY : MonoBehaviour
{
    public float offsetY=180f;
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0,Camera.main.transform.rotation.eulerAngles.y+offsetY,0);
    }
}
