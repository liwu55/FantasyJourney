using System;
using System.Collections;
using System.Collections.Generic;
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
