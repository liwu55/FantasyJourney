using System;
using System.Collections;
using System.Collections.Generic;
using Frame.UI;
using Frame.Utility;
using Photon.Pun;
using UnityEngine;
using EventType = Frame.Utility.EventType;
public class GameCtrl : SingleTonMono<GameCtrl>
{
    private Transform _spawnPoint;

    protected override void Awake()
    {
        base.Awake();
        _spawnPoint = GameObject.FindWithTag("SpawnPoint").transform;
        InitGame();
    }
    private void InitGame()
    {
        SpawnHero();
        EventCenter.Instance.AddListener(EventType.GameOver,GameOver);
    }
    private void SpawnHero()
    {
        string path = ConfigurationManager.Instance.GetPathByName("RedLoli");
        PhotonNetwork.Instantiate(path, _spawnPoint.position, Quaternion.identity);
    }
    private void GameOver()
    {
        UIManager.Instance.ShowModule("DoGameOverPanel");
    }
    private void OnApplicationQuit()
    {
        EventCenter.Instance.RemoveListener(EventType.GameOver,GameOver);
    }
}
