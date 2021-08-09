using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    private Vector3 targetToCamera;
    private void Awake()
    {
        targetToCamera = transform.position - target.transform.position;
    }

    private void Update()
    {
        Vector3 targetPoint = target.transform.position + targetToCamera;
        transform.position = Vector3.Lerp(transform.position, targetPoint, Time.deltaTime * 4);
    }
}
