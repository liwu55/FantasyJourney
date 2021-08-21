using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Frame.UI;
using Frame.Utility;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomlistPanel : UIModuleBase
{
   private ClientState previousState;

   private Dictionary<string, GameObject> cacheRooms;
   private UIWidget roomListParentTra;
   private UIWidget noRoomFoundObj;


   protected override void Awake()
   {
       base.Awake();
       cacheRooms = new Dictionary<string, GameObject>();
       roomListParentTra = FW("RoomList#");
       noRoomFoundObj = FW("NoRoomList#");
   }

   private void Start()
   {
       
       FW("BackButton#").Button.onClick.AddListener(() =>
       {
           UIManager.Instance.PopModule();
           //PhotonNetwork.LeaveLobby();
       });
       
       FW("FastPlayButton#").Button.onClick.AddListener(() =>
       {
            //随机加入没有密码的房间
           PhotonNetwork.JoinRandomRoom(new Hashtable{{ProtocolConst.ROOMPASSWORD,""}},0);
       });
       
       FW("CreateRoomButton#").Button.onClick.AddListener(() =>
       {
           UIManager.Instance.ShowModule("CreateRoomPanel");
           
       });
   }

 // private void Update()
 // {
 //    if (PhotonNetwork.NetworkClientState
 //        != previousState)
 //    {
 //      Debug.Log("现在状态是:"+PhotonNetwork.NetworkClientState);
 //    }
 //    previousState = PhotonNetwork.NetworkClientState;
 // }

   private void OnRoomListUpdate(List<RoomInfo> roomInfos)
   {
       Debug.Log("更新一次房间");
       for (int i = 0; i < roomInfos.Count; i++)
       {
           if (!roomInfos[i].RemovedFromList)
           {
               if (cacheRooms.ContainsKey(roomInfos[i].Name))
               {
                   //更新房间信息
                   cacheRooms[roomInfos[i].Name].GetComponent<RoomInfoSubModule>().UpdateRoomInfo(roomInfos[i]);
               }
               else
               {
                   var obj = ObjectPool.Instance.SpawnObj(
                       "RoomInfo", //对象名称
                       roomListParentTra.transform, //对象父对象
                       roomInfos[i]);//动态参数
                   //缓存对象
                   cacheRooms.Add(roomInfos[i].Name,obj);
               }
           }
           else
           {
               //从字典中清除该房间
               cacheRooms.Remove(roomInfos[i].Name);
               //回收对象
               ObjectPool.Instance.RecycleObj(cacheRooms[roomInfos[i].Name]);
           }
       }
       //检测是否有房间
       noRoomFoundObj.gameObject.SetActive(cacheRooms.Count == 0);
   }

   
   

   #region Moudle CallBacks

   public override void OnSpawn(object obj)
   {
       base.OnSpawn(obj);
       PhotonCallBackManager.instance.AddOnRoomListUpdateEvevt(OnRoomListUpdate);
   }

   public override void OnPause()
   {
    //  base.OnPause();
    //  PhotonCallBackManager.instance.RemoveOnRoomListUpdateEvevt(OnRoomListUpdate);
    //  RecycleRoomItems();
   }

   public override void OnResume()
   {
   // base.OnResume();
   // PhotonCallBackManager.instance.AddOnRoomListUpdateEvevt(OnRoomListUpdate);
   // PhotonNetwork.JoinLobby();
   }

   public override void OnRecycle()
   {
       base.OnRecycle();
       PhotonCallBackManager.instance.RemoveOnRoomListUpdateEvevt(OnRoomListUpdate);
       RecycleRoomItems();
   }

   #endregion

   
   private void RecycleRoomItems()
   {
       foreach (var item in cacheRooms)
       {
           //回收对象
           ObjectPool.Instance.RecycleObj(item.Value);
       }
       //清空缓存
       cacheRooms.Clear();
   }
}
