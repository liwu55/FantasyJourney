using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Frame.UI;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerInfoModule : UIModuleBase
{
   private Player currentPlayer;

   private UIWidget bgImg;
   private UIWidget roomMasterSign;
   private UIWidget playerName;
   private UIWidget readySignImg;

   private bool isReady;

   public bool IsReady
   {
      get => isReady;
      set => isReady = value;
   }


   public void PlayerInit(Player player)
   {
      //获取玩家信息
      currentPlayer = player;
     
      if (roomMasterSign == null)
      {
         bgImg = GetComponent<UIWidget>();
         roomMasterSign = FW("RoomMaster#");
         playerName = FW("PlayerName#");
         readySignImg = FW("ReadySign#");
      }
      
      //是否是房主
      if (player.IsMasterClient)
      {
         roomMasterSign.gameObject.SetActive(true);
      }
      else
      {
         roomMasterSign.gameObject.SetActive(false);
      }

      if (currentPlayer.IsLocal)
      {
         //bgImg.Img.color = Color.yellow;
      }
      
      //显示玩家名称
      playerName.Text.text = currentPlayer.NickName;
    
      //尝试获取准备属性
      object readyState = null;
      player.CustomProperties.TryGetValue("PlayerReadyState", out readyState);
      if (readyState == null || !(bool) readyState)
      {
         isReady = false;
      }
      else
      {
         isReady = true;
      }
      
   }

   public void OnChangeReady()
   {
      isReady = !isReady;
      currentPlayer.SetCustomProperties( 
         new Hashtable {
             {
                "PlayerReadyState",
                IsReady
             }});
   }

   public void UpdatePlayerReadyState(bool newReadyState)
   {
      //更新准备状态
      isReady = newReadyState;
      //界面更新
      readySignImg.gameObject.SetActive(isReady);
   }

   public override void OnSpawn(object obj)
   {
      PlayerInit(obj as Player);
   }
}
