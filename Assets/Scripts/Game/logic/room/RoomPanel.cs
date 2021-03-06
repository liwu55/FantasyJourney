using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Frame.UI;
using Frame.Utility;
using Game.flag;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using EventType = Frame.Utility.EventType;

public class RoomPanel : UIModuleBase
{
    private Dictionary<int, GameObject> cachePlayers;
    
    private UIWidget playerListParent;
    private UIWidget start1Button;
    private UIWidget start2Button;
    private UIWidget start3Button;
    private UIWidget roomName;
    private UIWidget backButton;
    private UIWidget readyButton;
    private Player currentPlayer;
    private PlayerInfoModule _playerInfoModule;

    protected override void Awake()
    {
        
        base.Awake();
        cachePlayers = new Dictionary<int, GameObject>();
        playerListParent = FW("PlayersList#");
        
        start1Button = FW("Start1Button#");
        start1Button.gameObject.SetActive(false);
        start1Button.Button.onClick.AddListener(() =>
        {
            ClearData();
            PhotonNetwork.LoadLevel("Flag");
            PhotonNetwork.CurrentRoom.IsOpen = false;
        });
        start2Button = FW("Start2Button#");
        start2Button.gameObject.SetActive(false);
        start2Button.Button.onClick.AddListener(() =>
        {
            ClearData();
            //加载场景
            PhotonNetwork.LoadLevel("BattleForFlag");
            PhotonNetwork.CurrentRoom.IsOpen = false;
        });
        start3Button = FW("Start3Button#");
        start3Button.gameObject.SetActive(false);
        start3Button.Button.onClick.AddListener(() =>
        {
            ClearData();
            //加载场景
            PhotonNetwork.LoadLevel("DoOneFight");
            PhotonNetwork.CurrentRoom.IsOpen = false;
        });
        

        backButton = FW("BackButton#");
        backButton.Button.onClick.AddListener(() =>
        {
            PhotonNetwork.LeaveRoom();
            
            
        });

        readyButton = FW("ReadyButton#");
        readyButton.Button.onClick.AddListener(() =>
        {
            //cachePlayers[PhotonNetwork.LocalPlayer.ActorNumber].transform.Find("ReadySign#").gameObject.SetActive(true);
            _playerInfoModule = cachePlayers[PhotonNetwork.LocalPlayer.ActorNumber].GetComponent<PlayerInfoModule>();
            _playerInfoModule.OnChangeReady();
            
        });

        roomName = FW("RoomName#");
        roomName.Text.text = PhotonNetwork.CurrentRoom.Name;
        
        //注册有玩家进入的回调函数
        EventCenter.Instance.AddListener<Player>(EventType.PlayerEntered,OnPlayerEntered);
        //注册有玩家离开的回调函数
        EventCenter.Instance.AddListener<Player>(EventType.PlayerLeft,OnPlayerLeft);
        //注册玩家属性更新的回调
        EventCenter.Instance.AddListener<Player,bool>(EventType.UpdatePlayerReadyState,updatePlayerReadyState);
        //注册判断游戏开始的事件
        EventCenter.Instance.AddListener(EventType.JudgmentStartGame,SetStartGameBtnShowOrHide);
    }

    private void ClearData()
    {
        EventCenter.Instance.Clear();
        SceneHeroes.Instance.Clear();
        UIManager.Instance.Clear();
        ObjectPool.Instance.Clear();
        HeroShow.Instance.Clear();
    }
    
    private void OnPlayerEntered(Player player)
    {
        CreateNewPlayerInfo(player);
        Debug.Log("玩家进入房间");
    }

    private void OnPlayerLeft(Player player)
    {
        Debug.Log("玩家退出房间");
        //回收对象
        ObjectPool.Instance.RecycleObj(cachePlayers[player.ActorNumber]);
        //从缓存中移除
        cachePlayers.Remove(player.ActorNumber);
    }

    private void CreateNewPlayerInfo(Player newPlayer)
    {
        //生成玩家信息对象
        GameObject playerInfo =
            ObjectPool.Instance.SpawnObj(
                "PlayerInfoPanel",
                playerListParent.transform,
                newPlayer);
        //缓存到字典
        cachePlayers.Add(
            newPlayer.ActorNumber,
            playerInfo);
    }

    private void updatePlayerReadyState(Player player, bool readyState)
    {
        try
        {
            //让更新准备状态的玩家更新界面
            cachePlayers[player.ActorNumber].GetComponent<PlayerInfoModule>().UpdatePlayerReadyState(readyState);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    
    /// <summary>
    /// 房间内玩家都已准备
    /// </summary>
    /// <returns></returns>
    private bool RoomPlayersAllReady()
    {
        Debug.Log("玩家个数:"+PhotonNetwork.PlayerList.Length);
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //获取当前玩家
            Player crtPlayer = PhotonNetwork.PlayerList[i];
            //准备状态
            object readyState = null;
            //获取玩家准备状态
            crtPlayer.CustomProperties.TryGetValue("PlayerReadyState", out readyState);
            //当前玩家没有准备
            if (readyState == null || !(bool) readyState)
                return false;
        }
        //所有人都准备了
        return true;
    }

    private bool CanStartGame()
    {
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
            return false;
        return RoomPlayersAllReady();
    }

    private void SetStartGameBtnShowOrHide()
    {
        /*if (PhotonNetwork.CurrentRoom.PlayerCount % 2 == 0)
        {
        }*/
        start1Button.gameObject.SetActive(CanStartGame());
        //start2Button.gameObject.SetActive(CanStartGame());
        
        /*if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            start3Button.gameObject.SetActive(CanStartGame());
        }*/
    }


    #region Module CallBacks

    public override void OnSpawn(object obj)
    {
        base.OnSpawn(obj);
        SetStartGameBtnShowOrHide();
        
        
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            CreateNewPlayerInfo(PhotonNetwork.PlayerList[i]);
        }
    }

    public override void OnRecycle()
    {
        base.OnRecycle();
        
        
        foreach (var item in cachePlayers)
        {
            //回收对象
            ObjectPool.Instance.RecycleObj(item.Value);
        }
        
        //清空列表
        cachePlayers.Clear();
    }

    private void OnDestroy()
    {
        //取消注册有玩家进入的回调函数
        EventCenter.Instance.RemoveListener<Player>(EventType.PlayerEntered,OnPlayerEntered);
        //取消注册有玩家离开的回调函数
        EventCenter.Instance.RemoveListener<Player>(EventType.PlayerLeft,OnPlayerLeft);
        //取消注册玩家属性更新的回调
        EventCenter.Instance.RemoveListener<Player,bool>(EventType.UpdatePlayerReadyState,updatePlayerReadyState);
        //取消注册判断游戏开始的事件
        EventCenter.Instance.RemoveListener(EventType.JudgmentStartGame,SetStartGameBtnShowOrHide);
    }

    #endregion
    
    
}
