using System;
using System.Collections.Generic;
using Frame.UI;
using Game;
using Game.DoOneFight.HealthPoint;
using Game.flag;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : UIModuleBase
{
    private Text OverMsg;
    private Player _player;
    protected override void Awake()
    {
        base.Awake();
        OverMsg = FW("OverMsg#").Text;
    }

    private void Update()
    {
        Time.timeScale -= Time.deltaTime * 10f;
    }

    public override void OnSpawn(object obj)
    {
        base.OnSpawn(obj);
        _player = (Player) obj;
        print("count = "+SceneHeroes.Instance.GetOthers(_player).Count);
        if (GameTimer.instance.totalTime <= 0)
        {
            if (SceneHeroes.Instance.GetOthers(_player)[0].GetTransform().GetComponent<HealthSystem>().currentHp < SceneHeroes.Instance.GetAll()[0].GetTransform().GetComponent<HealthSystem>().currentHp)
            {
                SetOverMsg(SceneHeroes.Instance.GetAll()[0].GetPhotonView().Owner.NickName + "胜利");
            }
            else
            {
                SetOverMsg(SceneHeroes.Instance.GetOthers(_player)[0].GetPhotonView().Owner.NickName + "胜利");
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