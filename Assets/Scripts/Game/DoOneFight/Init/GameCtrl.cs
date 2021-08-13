using System;
using System.Collections;
using System.Collections.Generic;
using Frame.Utility;
using Photon.Pun;
using UnityEngine;

public class GameCtrl : MonoBehaviour
{
    private Transform _spawnPoint;

    private void Awake()
    {
        _spawnPoint = GameObject.FindWithTag("SpawnPoint").transform;
        InitGame();
    }
    private void InitGame()
    {
        SpawnHero();
    }
    private void SpawnHero()
    {
        string path = ConfigurationManager.Instance.GetPathByName("RedLoli");
        PhotonNetwork.Instantiate(path, _spawnPoint.position, Quaternion.identity);
    }
    


}
