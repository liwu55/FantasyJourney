using System.Collections.Generic;
using Frame.Utility;
using Game.DoOneFight.State;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using EventType = Frame.Utility.EventType;

public class GameCtrl : SingleTonMono<GameCtrl>
{
    private Transform _spawnPoint;
    private Transform _spawnPointOther;
    private GameObject Player1;
    private GameObject Player2;
    protected override void Awake()
    {
        base.Awake();
        _spawnPoint = GameObject.FindWithTag("SpawnPoint").transform;
        _spawnPointOther = GameObject.FindWithTag("SpawnPointOther").transform;
        InitGame();
    }

    private void InitGame()
    {
        SpawnHero();
        EventCenter.Instance.AddListener(EventType.GameOver, GameOver);
    }

    private void SpawnHero()
    {
        string path1 = ConfigurationManager.Instance.GetPathByName("RedLoli");
        string path2 = ConfigurationManager.Instance.GetPathByName("Monster");
        
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject go = PhotonNetwork.Instantiate(path1, _spawnPoint.position, Quaternion.identity);
        }
        else
        {
            GameObject go =  PhotonNetwork.Instantiate(path2, _spawnPointOther.position, Quaternion.identity);
        }
    }

    private void GameOver()
    {
        UIManager.Instance.ShowModule("DoGameOverPanel");
    }

    private void OnApplicationQuit()
    {
        EventCenter.Instance.RemoveListener(EventType.GameOver, GameOver);
    }
}