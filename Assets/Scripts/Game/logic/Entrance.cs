using System;
using System.Reflection;
using Game;
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

    private void OnApplicationQuit()
    {
        
        DataBaseManager.Instance.OnQuit();
    }
}
