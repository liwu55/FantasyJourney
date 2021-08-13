using System;
using System.Collections;
using System.Collections.Generic;
using Frame.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MainModule : UIModuleBase
{
   private ClientState previousState;
   private UIWidget lobbyButton;
   private UIWidget playerName;

   private void Start()
   {
      playerName = FW("PlayerName#");
      lobbyButton = FW("LobbyButton#");
      lobbyButton.Button.onClick.AddListener(() =>
      {
         setNickName();
         PhotonNetwork.JoinLobby();
      });
   }

   private void Update()
   {
     
         if (PhotonNetwork.NetworkClientState
             != previousState)
         {
            Debug.Log("现在状态是:"+PhotonNetwork.NetworkClientState);
         }
         previousState = PhotonNetwork.NetworkClientState;
     

   }

   void setNickName()
   {
      //设置昵称
      //PhotonNetwork.LocalPlayer.NickName = playerName.Text.text;
      Debug.Log("我的昵称是:"+PhotonNetwork.LocalPlayer.NickName);
      
      //设置英雄
   }
}
