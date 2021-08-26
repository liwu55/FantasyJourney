using System;
using System.Collections.Generic;
using Frame.FSM;
using Frame.UI;
using Frame.Utility;
using Game;
using Game.DoOneFight.HealthPoint;
using Game.flag;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : UIModuleBase
{
    private Text OverMsg;
    private Player _player;
    private Button _backToMenu;
    private bool loadScene;
    protected override void Awake()
    {
        base.Awake();
        OverMsg = FW("OverMsg#").Text;
        _backToMenu = FW("_backToMenu#").Button;
        _backToMenu.onClick.AddListener(() =>
        {
            SceneHeroes.Instance.Clear();
            UIManager.Instance.Clear();
            ObjectPool.Instance.Clear();
            MonoHelper.Instance.Clear();
            PhotonNetwork.LeaveRoom();
            Time.timeScale = 1;
        });
    }

    private void Update()
    {
        Time.timeScale -= Time.deltaTime;
        if (Time.timeScale <= 0)
            Time.timeScale = 0;
        if (!loadScene && PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
        {
            loadScene = true;
            SceneManager.LoadScene("SampleScene");
        }
    }

    public override void OnSpawn(object obj)
    {
        base.OnSpawn(obj);
        _player = (Player) obj;
        print("count = "+SceneHeroes.Instance.GetOthers(_player).Count);
        if (GameTimer.instance.totalTime <= 0)
        {
            if (SceneHeroes.Instance.GetAll()[0].GetTransform().GetComponent<HealthSystem>().currentHp < SceneHeroes.Instance.GetAll()[1].GetTransform().GetComponent<HealthSystem>().currentHp)
            {
                SetOverMsg(SceneHeroes.Instance.GetAll()[1].GetPhotonView().Owner.NickName + "胜利");
            }
            else
            {
                SetOverMsg(SceneHeroes.Instance.GetAll()[0].GetPhotonView().Owner.NickName + "胜利");
            }
        }
        else
        {
            SetOverMsg(SceneHeroes.Instance.GetOthers(_player)[0].GetPhotonView().Owner.NickName + "胜利");
        }
    }

    private void SetOverMsg(string msg)
    {
        OverMsg.text = msg;
    }
}