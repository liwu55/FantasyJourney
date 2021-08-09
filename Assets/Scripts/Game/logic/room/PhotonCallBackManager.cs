using System;
using System.Collections;
using System.Collections.Generic;
using Frame.Utility;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using EventType = Frame.Utility.EventType;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonCallBackManager : MonoBehaviourPunCallbacks
{
    public static PhotonCallBackManager instance;
    private event  Action<List<RoomInfo>> onRoomListUpdate;
    private List<RoomInfo> roomInfosCache;
    private void Awake()
    {
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public List<RoomInfo> GetRoomListCache()
    {
        return roomInfosCache;
    }
    public void AddOnRoomListUpdateEvevt(Action<List<RoomInfo>> updateEvent)
    {
        if (updateEvent != null)
        {
            //绑定监听事件
            onRoomListUpdate += updateEvent;
        }
    }

    public void RemoveOnRoomListUpdateEvevt(Action<List<RoomInfo>> updateEvent)
    {
        if (updateEvent != null)
        {
            //解除绑定监听事件
            onRoomListUpdate -= updateEvent;
        }
    }
    
    
    #region Photon CallBacks

    public override void OnJoinedLobby()
    {
        UIManager.Instance.ShowModule("RoomlistPanel");
        Debug.Log("加入大厅");
    }

    public override void OnJoinedRoom()
    {
        UIManager.Instance.ShowModule("RoomPanel");
        Debug.Log("加入房间");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
   {
       //缓存
       roomInfosCache = roomList;
       if (onRoomListUpdate != null)
       {
           onRoomListUpdate(roomList);
       }
   }
    
   public override void OnLeftLobby()
   {
       UIManager.Instance.PopModule();
       Debug.Log("离开大厅");
   }

   public override void OnLeftRoom()
   {
     
       UIManager.Instance.PopModule();
       Debug.Log("离开房间");
   }

   public override void OnPlayerEnteredRoom(Player newPlayer)
   {
       //调用事件中心里的玩家进入事件
       EventCenter.Instance.Call(EventType.PlayerEntered,newPlayer);
       //调用判断是否可以开始游戏
       EventCenter.Instance.Call(EventType.JudgmentStartGame);
   }

   public override void OnPlayerLeftRoom(Player otherPlayer)
   {
       //调用事件中心里的玩家进入事件
       EventCenter.Instance.Call(EventType.PlayerLeft,otherPlayer);
       //调用判断是否可以开始游戏
       EventCenter.Instance.Call(EventType.JudgmentStartGame);
       
   }
    
   

   public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
   {
       object readyState = null;
       changedProps.TryGetValue("PlayerReadyState", out readyState);

       if (readyState != null)
       {
           //判断是否可以开始游戏
           EventCenter.Instance.Call(EventType.JudgmentStartGame);
           //调用事件
           EventCenter.Instance.Call(EventType.UpdatePlayerReadyState,targetPlayer,(bool)readyState);
       }
   }

   #endregion
}
