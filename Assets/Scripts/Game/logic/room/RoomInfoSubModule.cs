using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Frame.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomInfoSubModule : UIModuleBase
{
   private RoomInfo currentRoomInfo;

   private UIWidget nameText;
   private UIWidget numText;
   private UIWidget pwdobj;
   private UIWidget passwordInput;
   private UIWidget joinBtn;

   private object realPassword;

   public void RoomInit(RoomInfo info)
   {
      //接受房间信息
      currentRoomInfo = info;
      if (numText == null)
      {
         nameText = FW("RoomNameText#");
         numText = FW("PlayersNumText#");
         pwdobj = FW("RoomPwd#");
         passwordInput = FW("RoomPwdInputField#");
         joinBtn = FW("SureButton#");
         
         joinBtn.Button.onClick.AddListener(() =>
         {
            if (realPassword == null ||
                passwordInput.InputField.text == realPassword.ToString())
            {
               //加入房间
               PhotonNetwork.JoinRoom(info.Name);
            }
         });
      }
      
      //显示房间名称
      nameText.Text.text = info.Name;
      numText.Text.text = info.PlayerCount + "/" + info.MaxPlayers;
      //尝试获取密码
      info.CustomProperties.TryGetValue("RoomPassWord", out realPassword);
      //如果没有密码，关闭密码框
      if (realPassword == null || realPassword.ToString() == "")
      {
         pwdobj.gameObject.SetActive(false);
      }
   }

   public void UpdateRoomInfo(RoomInfo info)
   {
      //更新房间信息
      currentRoomInfo = info;
      //更新人数
      numText.Text.text = info.PlayerCount + "/" + info.MaxPlayers;
   }

   public void Dispose()
   {
      passwordInput.InputField.text = "";
      pwdobj.gameObject.SetActive(true);
   }

   public override void OnSpawn(object obj)
   {
      var info = obj as RoomInfo;
      RoomInit(info);
   }

   public override void OnRecycle()
   {
      Dispose();
   }
}
