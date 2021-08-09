using System;
using System.Reflection;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    private void Awake()
    {
        GameController.Instance.Init();
       
    }

    private void Start()
    {
        GameController.Instance.Entrance();
    }
}
